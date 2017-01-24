using System.Collections.Generic;
using System.Linq;
using BarricaeModelLib.GeneratedCode.Model;
using BarricaeModelLib.GeneratedCode.Model.Fields;
using BarricaeModelLib.GeneratedCode.View;

namespace BarricaeModelLib.GeneratedCode.Controller
{
    public class PawnController
    {
        private readonly InputView _inputView;
        private readonly OutputView _outputView;
        private readonly BarricadeController _barricadeController;
        private Pawn _selectedPawn;
        private readonly Game _game;

        public PawnController(Game game)
        {
            _inputView = new InputView();
            _outputView = new OutputView();
            _game = game;
            _barricadeController = new BarricadeController(_game);
        }


        /// <summary>
        /// Movement of the pawn logic
        /// </summary>
        /// returns true when done moving,
        /// reutrn false to reset movement
	    public bool PawnMovement(int roll)
        {
            var id = _inputView.AskPawn() - 1;
            _selectedPawn = _game.CurrentPlayer.Pawns[id];
            _selectedPawn.VisitFields = new List<Field> { _game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX] };

            var startLocationX = _selectedPawn.LocationX;
            var startLocationY = _selectedPawn.LocationY;

            var locationX = startLocationX;
            var locationY = startLocationY;


            if (_game.Fields[locationY, locationX] != null)
                _game.Fields[locationY, locationX].Pawn = null;

            for (var i = 0; i < roll; i++)
            {
                _outputView.ClearConsole();
                _outputView.ShowPlayer(_game.CurrentPlayer);
                _outputView.ShowBoard(_game.Fields);
                _outputView.ShowThrow(roll - i);

                if (_selectedPawn.LocationY == 0 && _selectedPawn.LocationX == 0)
                {
                    locationY = _game.CurrentPlayer.StartField.LocationY;
                    locationX = _game.CurrentPlayer.StartField.LocationX;

                    _game.Fields[locationY, locationX].TempIcon = true;
                    _selectedPawn.VisitFields.Add(_game.Fields[locationY, locationX]);
                    continue;
                }

                if (locationY == 11 && _selectedPawn.LocationX == 11)
                {
                    _selectedPawn.LocationY = 10;
                    _game.Fields[locationY, locationX].TempIcon = true;
                    continue;
                }

                _game.Fields[locationY, locationX].TempIcon = false;

                Field nextField;
                Field nexLocation;

                switch (_inputView.AskDirection())
                {
                    case "w":
                        nextField = _game.Fields[locationY - 1, locationX];
                        nexLocation = _game.Fields[locationY - 2, locationX];

                        if (!CanMakeMove(nextField, nexLocation, roll - i))
                        {
                            i--;
                            _game.Fields[locationY, locationX].TempIcon = true;
                            break;
                        }

                        nexLocation.TempIcon = true;
                        locationY -= 2;
                        break;

                    case "a":
                        nextField = _game.Fields[locationY, locationX - 1];
                        nexLocation = _game.Fields[locationY, _selectedPawn.LocationX - 2];

                        if (!CanMakeMove(nextField, nexLocation, roll - i))
                        {
                            i--;
                            _game.Fields[locationY, locationX].TempIcon = true;
                            break;
                        }

                        nexLocation.TempIcon = true;
                        locationX -= 2;
                        break;

                    case "s":
                        nextField = _game.Fields[locationY + 1, locationX];
                        nexLocation = _game.Fields[locationY + 2, locationX];

                        if (!CanMakeMove(nextField, nexLocation, roll - i))
                        {
                            i--;
                            _game.Fields[locationY, locationX].TempIcon = true;
                            break;
                        }

                        nexLocation.TempIcon = true;
                        locationY += 2;
                        break;

                    case "d":
                        nextField = _game.Fields[locationY, locationX + 1];
                        nexLocation = _game.Fields[locationY, locationX + 2];


                        if (!CanMakeMove(nextField, nexLocation, roll - i))
                        {
                            i--;
                            break;
                        }

                        nexLocation.TempIcon = true;
                        locationX += 2;
                        break;

                    case "":
                        _outputView.ClearConsole();
                        _selectedPawn.LocationX = startLocationX;
                        _selectedPawn.LocationY = startLocationY;
                        return false;

                    default:
                        i--;
                        _outputView.WrongDirection();
                        break;
                }
                _selectedPawn.VisitFields.Add(_game.Fields[locationY, locationX]);
            }

            var newField = _game.Fields[locationY, locationX];

            if (newField.Pawn != null)
            {
                var pawn = _game.Players.First(x => x.Color == newField.Pawn.Owner.Color)
                    .Pawns[newField.Pawn.Id];
                if (newField.LocationY > 7)
                {
                    pawn.LocationY = 0;
                    pawn.LocationX = 0;
                }
                else
                {
                    pawn.LocationY = 11;
                    pawn.LocationX = 11;
                }
            }

            newField.Pawn = _selectedPawn;
            newField.TempIcon = false;

            _selectedPawn.LocationX = locationX;
            _selectedPawn.LocationY = locationY;
            if (_game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX].GetType() == typeof(FinishField))
                _game.CurrentPlayer.Winner = true;
            return true;
        }

        private bool CanMakeMove(Field nextPath, Field nextLocation, int movesleft)
        {
            if (_selectedPawn.VisitFields.Contains(nextLocation)) return false;
            if (nextLocation.Pawn != null && movesleft == 1)
                if (nextLocation.Pawn.Owner == _game.CurrentPlayer) return false;
            if (nextPath == null || nextPath.GetType() != typeof(PathField)) return false;
            if (nextLocation.GetType() == typeof(RestField) && nextLocation.Pawn == null && movesleft > 1) return true;
            if (nextLocation.Barricade == null) return true;
            if (movesleft != 1) return false;

            _barricadeController.MoveBarricade(nextLocation.Barricade, nextLocation);
            return true;
        }
    }
}
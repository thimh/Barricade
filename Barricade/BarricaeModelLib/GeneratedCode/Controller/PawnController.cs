using System.Collections.Generic;
using System.Linq;
using BarricaeModelLib.GeneratedCode.Model;
using Model;
using View;

namespace BarricaeModelLib.GeneratedCode.Controller
{
    public class PawnController
    {
        private readonly InputView _inputView;
        private readonly OutputView _outputView;
        private readonly BarricadeController _barricadeController;
        private Pawn _selectedPawn;

        public Game Game { get; set; }

        public PawnController(Game game)
        {
            Game = game;
            _inputView = new InputView();
            _outputView = new OutputView();
            _barricadeController = new BarricadeController(Game);
        }

        /// <summary>
        /// Movement of the pawn logic
        /// </summary>
        /// returns true when done moving,
        /// reutrn false to reset movement
	    public bool PawnMovement(int roll)
        {
            var id = _inputView.AskPawn() - 1;
            _selectedPawn = Game.currentPlayer.Pawns.FirstOrDefault(x => x.Id == id);
            _selectedPawn.VisitFields = new List<Field> { Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX] };

            var startLocationX = _selectedPawn.LocationX;
            var startLocationY = _selectedPawn.LocationY;

            if (Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX] != null)
                Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX].Pawn = null;

            for (var i = 0; i < roll; i++)
            {
                _outputView.ClearConsole();
                _outputView.ShowPlayer(Game.currentPlayer);
                _outputView.ShowBoard(Game.Fields);
                _outputView.ShowThrow(roll - i);

                if (_selectedPawn.LocationY == 0 && _selectedPawn.LocationX == 0)
                {
                    _selectedPawn.LocationY = Game.currentPlayer.StartField.LocationY;
                    _selectedPawn.LocationX = Game.currentPlayer.StartField.LocationX;

                    Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX].TempIcon = true;
                    _selectedPawn.VisitFields.Add(Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX]);
                    continue;
                }

                if (_selectedPawn.LocationY == 11 && _selectedPawn.LocationX == 11)
                {
                    _selectedPawn.LocationY = 10;
                    Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX].TempIcon = true;
                    continue;
                }

                Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX].TempIcon = false;

                Field nextField;
                Field nexLocation;

                switch (_inputView.AskDirection())
                {
                    case "w":
                        nextField = Game.Fields[_selectedPawn.LocationY - 1, _selectedPawn.LocationX];
                        nexLocation = Game.Fields[_selectedPawn.LocationY - 2, _selectedPawn.LocationX];

                        if (!CanMakeMove(nextField, nexLocation, roll - i))
                        {
                            i--;
                            Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX].TempIcon = true;
                            break;
                        }

                        nexLocation.TempIcon = true;
                        _selectedPawn.LocationY -= 2;
                        break;

                    case "a":
                        nextField = Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX - 1];
                        nexLocation = Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX - 2];

                        if (!CanMakeMove(nextField, nexLocation, roll - i))
                        {
                            i--;
                            Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX].TempIcon = true;
                            break;
                        }

                        nexLocation.TempIcon = true;
                        _selectedPawn.LocationX -= 2;
                        break;

                    case "s":
                        nextField = Game.Fields[_selectedPawn.LocationY + 1, _selectedPawn.LocationX];
                        nexLocation = Game.Fields[_selectedPawn.LocationY + 2, _selectedPawn.LocationX];

                        if (!CanMakeMove(nextField, nexLocation, roll - i))
                        {
                            i--;
                            Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX].TempIcon = true;
                            break;
                        }

                        nexLocation.TempIcon = true;
                        _selectedPawn.LocationY += 2;
                        break;

                    case "d":
                        nextField = Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX + 1];
                        nexLocation = Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX + 2];


                        if (!CanMakeMove(nextField, nexLocation, roll - i))
                        {
                            i--;
                            break;
                        }

                        nexLocation.TempIcon = true;
                        _selectedPawn.LocationX += 2;
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
                _selectedPawn.VisitFields.Add(Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX]);
            }

            var newField = Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX];

            if (newField.Pawn != null)
            {
                var pawn = Game.Players.First(x => x.Color == newField.Pawn.Owner.Color)
                    .Pawns.First(x => x.Id == newField.Pawn.Id);
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
            if (Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX].GetType() == typeof(FinishField))
                Game.currentPlayer.Winner = true;
            return true;
        }

        private bool CanMakeMove(Field nextPath, Field nextLocation, int movesleft)
        {
            if (_selectedPawn.VisitFields.Contains(nextLocation)) return false;
            if (nextLocation.Pawn != null && movesleft == 1)
                if (nextLocation.Pawn.Owner == Game.currentPlayer) return false;
            if (nextPath == null || nextPath.GetType() != typeof(PathField)) return false;
            if (nextLocation.GetType() == typeof(RestField) && nextLocation.Pawn == null && movesleft > 1) return true;
            if (nextLocation.Barricade == null) return true;
            if (movesleft != 1) return false;

            _barricadeController.MoveBarricade(nextLocation.Barricade, nextLocation);
            return true;
        }
    }
}
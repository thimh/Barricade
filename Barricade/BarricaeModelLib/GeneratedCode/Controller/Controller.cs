using System;
using Model;
using System.Linq;
using BarricaeModelLib.GeneratedCode.Model;
using View;

namespace Controller
{
	public class Controller
	{
	    private readonly InputView _inputView;
	    private readonly OutputView _outputView;
	    private readonly Dice _dice;
	    private Pawn _selectedPawn;

	    public Game Game { get; set; }

	    public Controller()
	    {
            _dice = new Dice();
            _inputView = new InputView();
            _outputView = new OutputView();
	        SetupGame();
            GameRunning();
	    }

        /// <summary>
        /// setup the players and the fields
        /// </summary>
        public void SetupGame()
        {
            Game = new Game();
            var color = new Color[4] { Color.Red, Color.Green, Color.Yellow, Color.Blue };
            var playerAmmount = _inputView.AskPlayerAmmount();

            for (var i = 0; i < playerAmmount; i++)
                Game.Players.Add(new Player { Name = _inputView.AskPlayerName(), Color = color[i], ID = i});

            Game.BuildFields();

            Game.currentPlayer = Game.Players.FirstOrDefault(x => x.ID == 0);
        }

        /// <summary>
        /// loops gameturns til game is over
        /// </summary>
        public void GameRunning()
        {
            while (true)
                GameTurn();
        }

        /// <summary>
        /// logic for game turns
        /// </summary>
	    public void GameTurn()
        {
            _outputView.ClearConsole();
            
            var roll = _dice.Throw();
            
            while (true)
            {
                _outputView.ShowPlayer(Game.currentPlayer);
                _outputView.ShowBoard(Game.Fields);
                _outputView.ShowThrow(roll);
                if (PawnMovement(roll))
                    break;
            }

            //Set next player
            Game.ChangeTurn();
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

            var startLocationX = _selectedPawn.LocationX;
            var startLocationY = _selectedPawn.LocationY;

            if(Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX] !=null)
                Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX].Pawn = null;

            for (var i = 0; i < roll; i++)
            {
                _outputView.ClearConsole();
                _outputView.ShowPlayer(Game.currentPlayer);
                _outputView.ShowBoard(Game.Fields);
                _outputView.ShowThrow(roll-i);

                if (_selectedPawn.LocationY == 0 && _selectedPawn.LocationX == 0)
                {
                    _selectedPawn.LocationY = Game.currentPlayer.StartField.LocationY;
                    _selectedPawn.LocationX = Game.currentPlayer.StartField.LocationX;
                    
                    Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX].TempIcon = true;
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
                        _selectedPawn.LocationX = startLocationX;
                        _selectedPawn.LocationY = startLocationY;
                        return false;

                    default:
                        i--;
                        _outputView.WrongDirection();
                        break;
                }
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

            Game.currentPlayer.Move(_selectedPawn);
            newField.Pawn = _selectedPawn;
            newField.TempIcon = false;
            return true;
	    }

	    private bool CanMakeMove(Field nextField, Field nextLocation, int movesleft)
	    {
            if (nextField == null || nextField.GetType() != typeof(PathField)) return false;
	        if (nextLocation.GetType() == typeof(RestField) && nextLocation.Pawn == null && movesleft > 1) return true;
	        if (nextLocation.Barricade == null) return true;
	        if (movesleft != 1) return false;
            
            MoveBarricade(nextLocation.Barricade, nextLocation);
	        return true;
	    }

        public void MoveBarricade(Barricade barricade, Field currentField)
        {
            _outputView.ShowDirection();
            while (true)
            {
                currentField.Barricade = null;
                _outputView.ClearConsole();
                _outputView.ShowBoard(Game.Fields);

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W:
                        if (CanMoveBarricade(Game.Fields[currentField.LocationY - 1, currentField.LocationX]))
                        {
                            currentField = Game.Fields[currentField.LocationY - 2, currentField.LocationX];
                            currentField.Barricade = barricade;
                        }
                        break;

                    case ConsoleKey.S:
                        if (CanMoveBarricade(Game.Fields[currentField.LocationY + 1, currentField.LocationX]))
                        {
                            currentField = Game.Fields[currentField.LocationY + 2, currentField.LocationX];
                            currentField.Barricade = barricade;
                        }
                        break;

                    case ConsoleKey.A:
                        if (CanMoveBarricade(Game.Fields[currentField.LocationY, currentField.LocationX - 1]))
                        {
                            currentField = Game.Fields[currentField.LocationY, currentField.LocationX - 2];
                            currentField.Barricade = barricade;
                        }
                        break;

                    case ConsoleKey.D:
                        if (CanMoveBarricade(Game.Fields[currentField.LocationY, currentField.LocationX + 1]))
                        {
                            currentField = Game.Fields[currentField.LocationY, currentField.LocationX + 2];
                            currentField.Barricade = barricade;
                        }
                        break;

                    case ConsoleKey.Enter:
                        if (CanPlaceBarricade(currentField)) currentField.Barricade = barricade; return;
                        break;
                }
            }
        }

	    public bool CanMoveBarricade(Field nextField)
	    {
	        if (nextField == null || nextField.GetType() != typeof(PathField)) return false;

	        return true;
	    }

	    public bool CanPlaceBarricade(Field currentField)
	    {
	        if (currentField.GetType() == typeof(RestField) || currentField.GetType() == typeof(ForestField) || currentField.GetType() == typeof(FinishField)) return false;
	        if (currentField.Pawn != null || currentField.Barricade != null) return false;
	        if (currentField.LocationX == 14) return false;

	        return true;
	    }
    }
}
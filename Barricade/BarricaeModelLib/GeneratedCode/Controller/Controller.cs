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
            var color = new Color[4] { Color.Blue, Color.Green, Color.Red, Color.Yellow };
            var playerAmmount = _inputView.AskPlayerAmmount();

            for (var i = 0; i < playerAmmount; i++)
            {
                Game.Players.Add(new Player { Name = _inputView.AskPlayerName(), Color = color[i], ID = i});
            }

            Game.BuildFields();

            Game.currentPlayer = Game.Players.FirstOrDefault(x => x.ID == 0);
        }

        /// <summary>
        /// loops gameturns til game is over
        /// </summary>
        public void GameRunning()
        {
            while (true)
            {
                GameTurn();
            }
        }

        /// <summary>
        /// logic for game turns
        /// </summary>
	    public void GameTurn()
        {
            _outputView.ClearConsole();
            
            // roll the dice
            var roll = _dice.Throw();

            // move pawn
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

                    Game.currentPlayer.Move(_selectedPawn);
                    Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX].TempIcon = true;
                    continue;
                }

                Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX].TempIcon = false;
                Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX].Pawn = null;

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

            Game.currentPlayer.Move(_selectedPawn);
            Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX].Pawn = _selectedPawn;
            Game.Fields[_selectedPawn.LocationY, _selectedPawn.LocationX].TempIcon = false;
            return true;
	    }

	    private bool CanMakeMove(Field nextField, Field nextLocation, int movesleft)
	    {
            if (nextField == null || nextField.GetType() != typeof(PathField)) return false;
	        if (nextLocation.GetType() == typeof(RestField) && nextLocation.Pawn == null && movesleft > 1) return true;
	        if (nextLocation.Barricade == null) return true;
	        if (movesleft != 1) return false;
            
            MoveBarricade(nextLocation.Barricade, nextLocation);
	        nextLocation.Barricade = null;
	        return true;
	    }

        public void MoveBarricade(Barricade barricade, Field currentField)
        {
            _outputView.ShowDirection();
            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W:
                        currentField = Game.Fields[currentField.LocationY - 2, currentField.LocationX];
                        currentField.Barricade = barricade;
                        _outputView.ClearConsole();
                        _outputView.ShowBoard(Game.Fields);
                        break;

                    case ConsoleKey.S:
                        currentField = Game.Fields[currentField.LocationY + 2, currentField.LocationX];
                        currentField.Barricade = barricade;
                        _outputView.ClearConsole();
                        _outputView.ShowBoard(Game.Fields);
                        break;

                    case ConsoleKey.A:
                        currentField = Game.Fields[currentField.LocationY, currentField.LocationX - 2];
                        currentField.Barricade = barricade;
                        _outputView.ClearConsole();
                        _outputView.ShowBoard(Game.Fields);
                        break;

                    case ConsoleKey.D:
                        currentField = Game.Fields[currentField.LocationY, currentField.LocationX + 2];
                        currentField.Barricade = barricade;
                        _outputView.ClearConsole();
                        _outputView.ShowBoard(Game.Fields);
                        break;

                    case ConsoleKey.Enter:
                        return;
                }
            }
        }
    }
}
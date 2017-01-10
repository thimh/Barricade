using System.Security.Cryptography.X509Certificates;
using BarricaeModelLib.GeneratedCode.Model;

namespace Controller
{
	using Model;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using View;

	public class Controller
	{
	    #region Declerations
        
	    private readonly InputView _inputView;
	    private readonly OutputView _outputView;

	    private Dice _dice;

	    public virtual Game Game { get; set; }

	    #endregion

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
            var id = _inputView.AskPawn();
            id--;
            var selectedPawn = Game.currentPlayer.Pawns.FirstOrDefault(x => x.Id == id);

            var startLocationX = selectedPawn.LocationX;
            var startLocationY = selectedPawn.LocationY;

            for (var i = 0; i < roll; i++)
            {
                _outputView.ClearConsole();
                _outputView.ShowPlayer(Game.currentPlayer);
                _outputView.ShowBoard(Game.Fields);
                _outputView.ShowThrow(roll-i);

                if (selectedPawn.LocationY == 0 && selectedPawn.LocationX == 0)
                {
                    selectedPawn.LocationY = Game.currentPlayer.StartField.LocationY;
                    selectedPawn.LocationX = Game.currentPlayer.StartField.LocationX;

                    Game.currentPlayer.Move(selectedPawn);
                    Game.Fields[selectedPawn.LocationY, selectedPawn.LocationX].Pawn = selectedPawn;
                    continue;
                }

                Game.Fields[selectedPawn.LocationY, selectedPawn.LocationX].Pawn = null;

                switch (_inputView.AskDirection())
                {
                    case "w":
                        if ((Game.Fields[selectedPawn.LocationY - 1, selectedPawn.LocationX] == null ||
                            Game.Fields[selectedPawn.LocationY - 1, selectedPawn.LocationX].GetType() !=
                            typeof(PathField)) && Game.Fields[selectedPawn.LocationY - 2, selectedPawn.LocationX].Barricade != null)
                        {
                            i--;
                            break;
                        }
                        selectedPawn.LocationY -= 2;
                        break;
                    case "a":
                        if ((Game.Fields[selectedPawn.LocationY, selectedPawn.LocationX - 1] == null ||
                            Game.Fields[selectedPawn.LocationY, selectedPawn.LocationX - 1].GetType() !=
                            typeof(PathField)) && Game.Fields[selectedPawn.LocationY, selectedPawn.LocationX - 2].Barricade != null)
                        {
                            i--;
                            break;
                        }
                        selectedPawn.LocationX -= 2;
                        break;
                    case "s":
                        if ((Game.Fields[selectedPawn.LocationY + 1, selectedPawn.LocationX] == null ||
                            Game.Fields[selectedPawn.LocationY + 1, selectedPawn.LocationX].GetType() !=
                            typeof(PathField)) && Game.Fields[selectedPawn.LocationY + 2, selectedPawn.LocationX].Barricade != null)
                        {
                            i--;
                            break;
                        }
                        selectedPawn.LocationY += 2;
                        break;
                    case "d":
                        if ((Game.Fields[selectedPawn.LocationY, selectedPawn.LocationX + 1] == null ||
                            Game.Fields[selectedPawn.LocationY, selectedPawn.LocationX + 1].GetType() !=
                            typeof(PathField)) && Game.Fields[selectedPawn.LocationY, selectedPawn.LocationX + 2].Barricade != null)
                        {
                            i--;
                            break;
                        }
                        selectedPawn.LocationX += 2;
                        break;
                    case "":
                        selectedPawn.LocationX = startLocationX;
                        selectedPawn.LocationY = startLocationY;
                        return false;
                    default:
                        i--;
                        _outputView.WrongDirection();
                        continue;
                }

                Game.currentPlayer.Move(selectedPawn);
                Game.Fields[selectedPawn.LocationY, selectedPawn.LocationX].Pawn = selectedPawn;
            }
            return true;
	    }
    }
}
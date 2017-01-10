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

	    public virtual Dice Dice { get; set; }

	    private InputView inputView;
	    private OutputView outputView;

	    public virtual Game Game { get; set; }

	    #endregion

	    public Controller()
	    {
            inputView = new InputView();
            outputView = new OutputView();
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
            var playerAmmount = inputView.AskPlayerAmmount();
            for (var i = 0; i < playerAmmount; i++)
            {
                Game.Players.Add(new Player { Name = inputView.AskPlayerName(), Color = color[i], ID = i});
            }

            outputView.ShowBoard(Game.Fields);

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
            // roll the dice
            var roll = Dice.Eyes;

            // move pawn
            while (true)
            {
                outputView.ShowBoard(Game.Fields);
                outputView.ShowThrow(roll);
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
            var selectedPawn = Game.currentPlayer.Pawns.FirstOrDefault(x => x.Id == inputView.AskPawn());

            var startLocationX = selectedPawn.LocationX;
            var startLocationY = selectedPawn.LocationY;

            for (var i = 0; i < roll; i++)
            {
                if (selectedPawn.LocationY == 0 && selectedPawn.LocationX == 0)
                {
                    selectedPawn.LocationY = Game.currentPlayer.StartField.LocationY;
                    selectedPawn.LocationX = Game.currentPlayer.StartField.LocationX;
                    continue;
                }

                switch (inputView.AskDirection())
                {
                    case "w":
                        selectedPawn.LocationY -= 2;
                        break;
                    case "a":
                        selectedPawn.LocationX -= 2;
                        break;
                    case "s":
                        selectedPawn.LocationY += 2;
                        break;
                    case "d":
                        selectedPawn.LocationX += 2;
                        break;
                    case "":
                        selectedPawn.LocationX = startLocationX;
                        selectedPawn.LocationY = startLocationY;
                        return false;
                    default:
                        outputView.WrongDirection();
                        continue;
                }

                Game.currentPlayer.Move(selectedPawn);
                Game.Fields[5, 5].Pawn = selectedPawn;

                outputView.ShowBoard(Game.Fields);
                outputView.ShowThrow(roll);
            }

            return false;
	    }
    }
}


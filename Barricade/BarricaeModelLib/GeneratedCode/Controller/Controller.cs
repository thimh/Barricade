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
            Console.Clear();
            outputView.ShowBoard(Game.Fields);
            // roll the dice
            var roll = Dice.Eyes;
            outputView.ShowThrow(roll);

            // move pawn
            while (true)
            {
                if(PawnMovement(roll))
                    break;
            }

            //Set next player
            Game.ChangeTurn();
        }

	    public bool PawnMovement(int roll)
	    {
            var selectedPawn = Game.currentPlayer.Pawns.FirstOrDefault(x => x.Id == inputView.AskPawn());

            var startLocationX = selectedPawn.LocationX;
            var startLocationY = selectedPawn.LocationY;

            for (var i = 0; i < roll; i++)
            {
                switch (inputView.AskDirection())
                {
                    case "w":
                        break;
                    case "a":
                        break;
                    case "s":
                        break;
                    case "d":
                        break;
                    case "":
                        selectedPawn.LocationX = startLocationX;
                        selectedPawn.LocationY = startLocationY;
                        return false;
                    default:
                        i--;
                        outputView.WrongDirection();
                        break;
                }
            }

            Game.currentPlayer.Move(selectedPawn);
            Game.Fields[5, 5].Pawn = selectedPawn;

            return false;
	    }

	    /*public List<Field> EndingFields(Pawn selectedPawn, int eyes)
	    {
            var endFields = new List<Field>();
	        if (selectedPawn.LocationX == 0 && selectedPawn.LocationY == 0)
	        {
	            selectedPawn.LocationX = Game.currentPlayer.StartField.LocationX;
	            selectedPawn.LocationY = Game.currentPlayer.StartField.LocationY;
	            eyes--;
	        }

	        if (eyes == 0) endFields.Add(Game.currentPlayer.StartField);
	        bool walkedAllPaths = false;
            while(!walkedAllPaths)
	        {
	            for (var i = 0; i < eyes; i++)
	            {
	                //West
	                if (Game.Fields[selectedPawn.LocationX - 1, selectedPawn.LocationY].GetType() == typeof(PathField))
	                {

	                }
	                //East
	                else if (Game.Fields[selectedPawn.LocationX + 1, selectedPawn.LocationY].GetType() == typeof(PathField))
	                {

	                }
	                //North
	                else if (Game.Fields[selectedPawn.LocationX, selectedPawn.LocationY - 1].GetType() == typeof(PathField))
	                {

	                }
	                //South
	                else if (Game.Fields[selectedPawn.LocationX, selectedPawn.LocationY + 1].GetType() == typeof(PathField))
	                {

	                }



	            }
	        }

	        return endFields;
	    }*/
    }
}


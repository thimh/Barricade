using System.Security.Cryptography.X509Certificates;

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

            outputView.showBoard(Game.Fields);

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
            var roll = Dice.Eyes;

            var selectedPawn = Game.currentPlayer.Pawns.FirstOrDefault(x => x.Id == inputView.AskPawn());
            
            Game.currentPlayer.Move(selectedPawn);
            Game.Fields[5, 5].Pawn = selectedPawn;

            Game.ChangeTurn();
        }

	    public string PawnMovement()
	    {

	        return null;
	    }
    }
}


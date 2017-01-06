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
	    public virtual Dice Dice { get; set; }

	    private InputView inputView;
	    private OutputView outputView;

	    public virtual Game Game { get; set; }

	    public Controller()
	    {
            inputView = new InputView();
            outputView = new OutputView();
	        SetupGame();
            GameRunning();
	    }

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
        }

        public virtual void GameRunning()
        {
            while (true)
            {
                
            }
        }
    }
}


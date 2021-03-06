﻿using System.Linq;
using BarricaeModelLib.GeneratedCode.Model;
using BarricaeModelLib.GeneratedCode.View;

namespace BarricaeModelLib.GeneratedCode.Controller
{
	public class GameController
	{
	    private readonly InputView _inputView;
	    private readonly OutputView _outputView;
	    private readonly Dice _dice;
	    private readonly PawnController _pawnController;

	    public Game Game { get; set; }

	    public GameController()
	    {
            _dice = new Dice();
            _inputView = new InputView();
            _outputView = new OutputView();
	        SetupGame();
            _pawnController = new PawnController(Game);
            GameRunning();
        }

        /// <summary>
        ///     setup the players and the fields
        /// </summary>
        public void SetupGame()
        {
            Game = new Game();
            var color = new[] {Color.Red, Color.Green, Color.Yellow, Color.Blue};
            var playerAmmount = _inputView.AskPlayerAmmount();

            for (var i = 0; i < playerAmmount; i++)
                Game.Players.Add(new Player
                {
                    Name = _inputView.AskPlayerName(),
                    Color = color[i],
                    ID = i
                });

            Game.BuildFields();

            Game.CurrentPlayer = Game.Players.FirstOrDefault(x => x.ID == 0);
        }

        /// <summary>
        ///     loops gameturns til game is over
        /// </summary>
        public void GameRunning()
        {
            while (true)
                foreach (var player in Game.Players)
                    if (!player.Winner)
                    {
                        GameTurn();
                    }
                    else
                    {
                        _outputView.WinScreen(player);
                        return;
                    }
        }

        /// <summary>
        ///     logic for game turns
        /// </summary>
        public void GameTurn()
        {
            _outputView.ClearConsole();

            var roll = _dice.Throw();

            while (true)
            {
                _outputView.ShowPlayer(Game.CurrentPlayer);
                _outputView.ShowBoard(Game.Fields);
                _outputView.ShowThrow(roll);
                if (_pawnController.PawnMovement(roll))
                    break;
            }

            //Set next player
            Game.ChangeTurn();
        }
    }
}
﻿using System.Security.Cryptography.X509Certificates;
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

            _outputView.ShowBoard(Game.Fields);

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
            var roll = _dice.Throw();

            // move pawn
            while (true)
            {
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
            var selectedPawn = Game.currentPlayer.Pawns.FirstOrDefault(x => x.Id == id);

            var startLocationX = selectedPawn.LocationX;
            var startLocationY = selectedPawn.LocationY;

            for (var i = 0; i < roll; i++)
            {
                _outputView.ShowBoard(Game.Fields);
                _outputView.ShowThrow(roll);

                if (selectedPawn.LocationY == 0 && selectedPawn.LocationX == 0)
                {
                    selectedPawn.LocationY = Game.currentPlayer.StartField.LocationY;
                    selectedPawn.LocationX = Game.currentPlayer.StartField.LocationX;

                    Game.currentPlayer.Move(selectedPawn);
                    Game.Fields[selectedPawn.LocationY, selectedPawn.LocationX].Pawn = selectedPawn;
                    continue;
                }
                
                switch (_inputView.AskDirection())
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


﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using View;

namespace Model
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class Game
	{
	    public Player currentPlayer { get; set; }

	    public Board Board { get; set; }

	    public List<Player> Player { get; set; }

	    public IEnumerable<Barricade> Barricade { get; set; }

	    private InputView input;

        public Game()
	    {
	        Board = new Board();
	        input = new InputView();

            SetupGame();
	        Console.ReadLine();
	    }

	    public void SetPlayers(Player player)
	    {
	        Player.Add(player);
	    }

		public void ChangeTurn()
		{
			throw new System.NotImplementedException();
		}

	    public void StartGame()
	    {
	        throw new NotImplementedException();
	    }

	    public void SetupGame()
	    {
	        for (int i = 0; i < input.AskPlayerAmmount(); i++)
	        {
	            SetPlayers(new Player {Color = input.AskPlayerColor()});
	        }
	    }

	}
}


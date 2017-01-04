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
        public virtual List<Field> Field { get; set; }
        
	    public List<Player> Player { get; set; }

	    public IEnumerable<Barricade> Barricade { get; set; }

	    private InputView input;

	    private Color[] color;

        public Game()
	    {
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
            Player = new List<Player>();
            color = new Color[4] { Color.Blue, Color.Green, Color.Red, Color.Yellow };
            int playerAmmount = input.AskPlayerAmmount();
            for (int i = 0; i < playerAmmount; i++)
	        {
	            SetPlayers(new Player {Name = input.AskPlayerName(), Color = color[i]});
	        }

	        buildFields();
        }

	    private void buildFields()
	    {
	        for (int i = 0; i < 61; i++)
	        {
	            
	        }
	    }
	}
}


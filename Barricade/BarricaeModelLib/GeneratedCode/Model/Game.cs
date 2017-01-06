﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Dynamic;
using System.IO;
using BarricaeModelLib.GeneratedCode.Model;
using View;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
	public class Game
	{
	    public Player currentPlayer { get; set; }
        public virtual List<Field> Field { get; set; }
        
	    public List<Player> Players { get; set; }

	    public IEnumerable<Barricade> Barricade { get; set; }

	    private InputView input;

	    private Color[] color;

	    public Field[,] Fields;

        public Game()
        {
            Players = new List<Player>();
            Fields = new Field[14,21];
            BuildFields();
        }

        /// <summary>
        /// Build a field array
        /// </summary>
	    private void BuildFields()
	    {

	        var file = System.IO.File.ReadAllText(@"..\..\Board\Baricade.txt");
	        string[] lines = System.IO.File.ReadAllLines(@"..\..\Board\Baricade.txt");

            var linecount = 0;
	        foreach (var line in lines)
	        {
	            for (int i = 0; i < line.Length; i++)
	            {
	                Field field = null;
	                switch (line[i])
	                {
                        case 'w':
                            field = new FinishField();
	                        break;
                        case 'x':
                            field = new Field();
	                        break;
                        case 'o':
                            field = new Field() {Barricade = new Barricade()};
	                        break;
                        case 's':
                            field = new RestField();
	                        break;
                        case 'f':
                            field = new ForestField();
	                        break;
                        case 'r':
                            field = new StartField {Color = Color.Red};
	                        break;
                        case 'y':
                            field = new StartField { Color = Color.Yellow };
                            break;
                        case 'g':
                            field = new StartField { Color = Color.Green };
                            break;
                        case 'b':
                            field = new StartField { Color = Color.Blue };
                            break;
                        case '-':
                            field = new PathField();
	                        break;
                        case ' ':
	                        field = null;
	                        break;
                        default:
	                        break;
	                }

	                Fields[linecount, i] = field;
	            }
	            linecount++;
	        }
	    }
        
        /// <summary>
        /// change player to next in list
        /// </summary>
		public void ChangeTurn()
		{
		    int nextPlayer = currentPlayer.ID++;
		    if (nextPlayer >= Players.Count) nextPlayer = 0;
		    currentPlayer = Players.FirstOrDefault(x => x.ID == nextPlayer);
		}
	}
}

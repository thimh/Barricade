﻿
using BarricaeModelLib.GeneratedCode.Model.Fields;
using System.Collections.Generic;
using System.Linq;
using BarricaeModelLib.GeneratedCode.Model;

namespace Model
{
	public class Game
	{
	    public Player currentPlayer { get; set; }
        
	    public List<Player> Players { get; set; }

	    public List<Barricade> Barricades { get; set; }

        public Field[,] Fields;

        public Game()
        {
            Players = new List<Player>();
            Barricades = new List<Barricade>();
            Fields = new Field[16,23];
        }

        /// <summary>
        /// Build a field array
        /// </summary>
	    public void BuildFields()
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
                            field = new FinishField {LocationX = i, LocationY = linecount};
	                        break;
                        case 'x':
                            field = new Field { LocationX = i, LocationY = linecount };
	                        break;
                        case 'o':
	                        var barricade = new Barricade();
                            Barricades.Add(barricade);
                            field = new Field {Barricade = barricade, LocationX = i, LocationY = linecount };
	                        break;
                        case 's':
                            field = new RestField { LocationX = i, LocationY = linecount };
	                        break;
                        case 'f':
                            field = new ForestField { LocationX = i, LocationY = linecount };
	                        break;
                        case 'r':
                            field = new StartField {Color = Color.Red, LocationX = i, LocationY = linecount };
	                        if (Players.Any(x => x.Color == Color.Red))
	                            Players.FirstOrDefault(x => x.Color == Color.Red).StartField = (StartField)field;
	                        break;
                        case 'y':
                            field = new StartField { Color = Color.Yellow, LocationX = i, LocationY = linecount };
                            if (Players.Any(x => x.Color == Color.Yellow))
                                Players.FirstOrDefault(x => x.Color == Color.Yellow).StartField = (StartField)field;
                            break;
                        case 'g':
                            field = new StartField { Color = Color.Green, LocationX = i, LocationY = linecount };
                            if (Players.Any(x => x.Color == Color.Green))
                                Players.FirstOrDefault(x => x.Color == Color.Green).StartField = (StartField)field;
                            break;
                        case 'b':
                            field = new StartField { Color = Color.Blue, LocationX = i, LocationY = linecount };
                            if (Players.Any(x => x.Color == Color.Blue))
                                Players.FirstOrDefault(x => x.Color == Color.Blue).StartField = (StartField)field;
                            break;
                        case '-':
                            field = new PathField { LocationX = i, LocationY = linecount };
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
		    int nextPlayer = currentPlayer.ID+1;
		    if (nextPlayer >= Players.Count) nextPlayer = 0;
		    currentPlayer = Players.Find(x => x.ID == nextPlayer);
		}
	}
}


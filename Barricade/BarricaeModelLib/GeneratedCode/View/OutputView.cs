﻿using System;
using BarricaeModelLib.GeneratedCode.Model;
using BarricaeModelLib.GeneratedCode.Model.Fields;

namespace BarricaeModelLib.GeneratedCode.View
{
    public class OutputView
	{
	    public void ShowBoard(Field[,] fields)
	    {
            Console.WriteLine("");
            for (int y = 0; y < 15; y++) 
	        {
                for (int x = 0; x < 23; x++)
                {
                    Field field = fields[y, x];
                    if (field == null)
                    {
                        Console.Write("  ");
                    }
                    else if (field.TempIcon)
                    {
                        Console.Write("  ");
                    }
                    else if (field.Barricade != null)
                    {
                        Console.Write(field.Barricade.Icon);
                    }
                    else if (field.Pawn != null)
                    {
                        if(field.Pawn.Owner.Color == Color.Blue)
                            Console.ForegroundColor = ConsoleColor.Blue;
                        if (field.Pawn.Owner.Color == Color.Red)
                            Console.ForegroundColor = ConsoleColor.Red;
                        if (field.Pawn.Owner.Color == Color.Green)
                            Console.ForegroundColor = ConsoleColor.Green;
                        if (field.Pawn.Owner.Color == Color.Yellow)
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(field.Pawn.Icon);
                        Console.ResetColor();
                    }
                    else
	                    Console.Write(fields[y,x].Icon);
	            }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void ShowThrow(int eyes)
	    {
	        Console.WriteLine("Moves Left: " + eyes);
	    }

	    public void WrongDirection()
	    {
	        Console.WriteLine("Wrong Direction");
	    }

	    public void ShowPlayer(Player gameCurrentPlayer)
	    {
	        Console.WriteLine("The Current Player = " + gameCurrentPlayer.Name);
            Console.WriteLine("color = " + gameCurrentPlayer.Color.ToString());
	    }

	    public void ClearConsole()
	    {
	        Console.Clear();
	    }

	    public void ShowDirection()
	    {
	        Console.WriteLine("Move barricade with w,a,s,d");
	    }

	    public void WinScreen(Player player)
	    {
            ClearConsole();
	        Console.WriteLine(player.Name + " has won the game!");
	    }
	}
}


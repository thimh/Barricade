﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace View
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class InputView
	{
	    public int AskPlayerAmmount()
	    {
	        Console.WriteLine("How many players?");
	        return Convert.ToInt16(Console.ReadLine());
	    }

	    public string AskPlayerName()
        {
            Console.WriteLine("What will be your player name?");
            return Console.ReadLine();
        }

	    public int AskPawn()
	    {
            Console.WriteLine("What pawn do you whish to move?");
            Console.WriteLine("1,2,3,4");
            return Convert.ToInt16(Console.ReadLine());
        }

	    public string AskDirection()
	    {
	        Console.WriteLine("What direction do you want to move?");
            Console.WriteLine("W,A,S,D for direction, empty to reset.");
            return Console.ReadLine();
	    }

	    public int AskBarricadeLocationY()
	    {
            Console.WriteLine("Set new barricade Y location?");
            return Convert.ToInt16(Console.ReadLine());
        }

        public int AskBarricadeLocationX()
        {
            Console.WriteLine("Set new barricade X location?");
            return Convert.ToInt16(Console.ReadLine());

        }
    }
}

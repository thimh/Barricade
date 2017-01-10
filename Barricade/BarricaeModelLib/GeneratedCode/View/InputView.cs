using System;

namespace View
{
	public class InputView
	{
	    public int AskPlayerAmmount()
	    {
	        Console.WriteLine("How many players?");
	        var players = Convert.ToInt16(Console.ReadLine());
	        while (players > 4)
	        {
                Console.WriteLine("Max players = 4");
                players = Convert.ToInt16(Console.ReadLine());
            }
	        return players;
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

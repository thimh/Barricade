using System;

namespace BarricaeModelLib.GeneratedCode.Model
{
    class Dice
	{
	    private readonly Random _r =new Random();

        public int Eyes { get; set; }

	    public int Throw()
	    {
	        Eyes = _r.Next(1, 7);
	        return Eyes;
	    }

	}
}


﻿namespace Model
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class Dice
	{
	    public static int Eyes => new Random().Next(1,6);
	}
}


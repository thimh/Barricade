﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
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
		public virtual Dice Dice
		{
			get;
			set;
		}

		public virtual InputView InputView
		{
			get;
			set;
		}

		public virtual OutputView OutputView
		{
			get;
			set;
		}

		public virtual Game Game
		{
			get;
			set;
		}

		public virtual void Go()
		{
			throw new System.NotImplementedException();
		}

	    public Controller()
	    {

        }

        private void buildFields()
        {
        }

    }
}


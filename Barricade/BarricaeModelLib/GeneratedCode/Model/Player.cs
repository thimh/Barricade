﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Player
{
    public virtual Pawn[] Pawns { get; set; }
    public virtual Color Color { get; set; }
    public string Name { get; set; }
    public int ID { get; set; }
    public virtual StartField StartField { get; set; }
    public bool Winner { get; set; }

    public Player()
    {
        Pawns = new Pawn[4];
        Pawns[0] = new Pawn { Icon = "1 ", Id = 0, Owner = this};
        Pawns[1] = new Pawn { Icon = "2 ", Id = 1, Owner = this};
        Pawns[2] = new Pawn { Icon = "3 ", Id = 2, Owner = this};
        Pawns[3] = new Pawn { Icon = "4 ", Id = 3, Owner = this};
    }
}


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
    public virtual List<Pawn> Pawn { get; set; }

    public virtual Color Color { get; set; }

    public string Name { get; set; }

    public virtual IEnumerable<StartField> StartField { get; set; }

    public Player()
    {
        if (Color == Color.Blue)
        {
            Pawn = new List<Pawn> {
            new Pawn{Id = 1, Icon = "B1"},
            new Pawn{Id = 2, Icon = "B2"},
            new Pawn{Id = 3, Icon = "B3"},
            new Pawn{Id = 4, Icon = "B4"}};
        }
        if (Color == Color.Yellow)
        {
            Pawn = new List<Pawn> {
            new Pawn{Id = 1, Icon = "Y1"},
            new Pawn{Id = 2, Icon = "Y2"},
            new Pawn{Id = 3, Icon = "Y3"},
            new Pawn{Id = 4, Icon = "Y4"}};
        }
        if (Color == Color.Red)
        {
            Pawn = new List<Pawn> {
            new Pawn{Id = 1, Icon = "R1"},
            new Pawn{Id = 2, Icon = "R2"},
            new Pawn{Id = 3, Icon = "R3"},
            new Pawn{Id = 4, Icon = "R4"}};
        }
        if (Color == Color.Green)
        {
            Pawn = new List<Pawn> {
            new Pawn{Id = 1, Icon = "G1"},
            new Pawn{Id = 2, Icon = "G2"},
            new Pawn{Id = 3, Icon = "G3"},
            new Pawn{Id = 4, Icon = "G4"}};
        }
    }

    public void Move()
    {
        throw new NotImplementedException();
    }

}


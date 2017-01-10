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

    public Player()
    {
        Pawns = new Pawn[4];
        if (Color == Color.Blue)
        {
            Pawns[0] = new Pawn { Icon = "B1", Id = 0};
            Pawns[1] = new Pawn { Icon = "B2", Id = 1};
            Pawns[2] = new Pawn { Icon = "B3", Id = 2};
            Pawns[3] = new Pawn { Icon = "B4", Id = 3};
        }
        if (Color == Color.Yellow)
        {
            Pawns[0] = new Pawn { Icon = "Y1", Id = 0};
            Pawns[1] = new Pawn { Icon = "Y2", Id = 1};
            Pawns[2] = new Pawn { Icon = "Y3", Id = 2};
            Pawns[3] = new Pawn { Icon = "Y4", Id = 3};
        }
        if (Color == Color.Red)
        {
            Pawns[0] = new Pawn { Icon = "R1", Id = 0};
            Pawns[1] = new Pawn { Icon = "R2", Id = 1};
            Pawns[2] = new Pawn { Icon = "R3", Id = 2};
            Pawns[3] = new Pawn { Icon = "R4", Id = 3};
        }
        if (Color == Color.Green)
        {
            Pawns[0] = new Pawn { Icon = "G1", Id = 0};
            Pawns[1] = new Pawn { Icon = "G2", Id = 1};
            Pawns[2] = new Pawn { Icon = "G3", Id = 2};
            Pawns[3] = new Pawn { Icon = "G4", Id = 3};
        }
    }

    public void Move(Pawn pawn)
    {
        Pawns[pawn.Id] = pawn;
    }
}


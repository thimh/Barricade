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

public class Field
{
    public virtual bool isOccupied { get; set; }
    public virtual string Icon { get { return "X "; }}
    public virtual bool canHaveBarricade { get; set; }
    public virtual Pawn Pawn { get; set; }
    public virtual Barricade Barricade { get; set; }
}


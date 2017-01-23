using System.Collections.Generic;
using BarricaeModelLib.GeneratedCode.Model.Fields;

public class Pawn
{
    public int LocationX { get; set; }
    public int LocationY { get; set; }
    public int Id { get; set; }
    public string Icon { get; set; }
    public Player Owner { get; set; }
    public List<Field> VisitFields { get; set; }
}


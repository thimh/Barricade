using BarricaeModelLib.GeneratedCode.Model.Fields;

namespace BarricaeModelLib.GeneratedCode.Model
{
    public class Player
    {
        public Pawn[] Pawns { get; set; }
        public Color Color { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
        public StartField StartField { get; set; }
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
}


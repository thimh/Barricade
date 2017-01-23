namespace BarricaeModelLib.GeneratedCode.Model.Fields
{
    public abstract class BaseField
    {
        public virtual string Icon { get; }
        public virtual bool canHaveBarricade { get; set; }
        public virtual Pawn Pawn { get; set; }
        public virtual Barricade Barricade { get; set; }
        public virtual int LocationX { get; set; }
        public virtual int LocationY { get; set; }
        public virtual bool TempIcon { get; set; }
    }
}

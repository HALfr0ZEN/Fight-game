namespace Hero_and_Dragon.Items
{
    public abstract class Defensive : Item
    {
        public int Defense { get; }

        protected Defensive(int weight, string name, int defense) : base(weight, name)
        {
            Defense = defense;
        }

        protected override double GetComparatorValue()
        {
            return Defense;
        }
    }
}
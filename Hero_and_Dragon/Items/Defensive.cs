namespace Hero_and_Dragon.Items
{
    public abstract class Defensive : Item
    {
        public int Defense { get; }

        protected Defensive(int weight, string name, int defense) : base(weight, name)
        {
            Defense = defense;
        }

        /// <summary>
        /// Comparator value as defense
        /// </summary>
        /// <returns>Defense</returns>
        protected override double GetComparatorValue() => Defense;
    }
}
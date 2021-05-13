using System;

namespace Hero_and_Dragon.Items
{
    public abstract class Offensive : Item
    {
        public int Damage { get; }

        protected Offensive(int weight, string name, int damage) : base(weight, name)
        {
            Damage = damage;
        }
        
        /// <summary>
        /// Comparator value as damage
        /// </summary>
        /// <returns>Damage</returns>
        protected override double GetComparatorValue() => Damage;
    }
}
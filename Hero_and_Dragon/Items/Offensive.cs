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
        
        protected override double GetComparatorValue()
        {
            return Damage;
        }
    }
}
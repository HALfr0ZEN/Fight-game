using System;

namespace Hero_and_Dragon.Items
{
    public abstract class Item : IComparable<Item>
    {
        protected readonly int Weight;
        protected readonly string Name;

        protected Item(int weight, string name)
        {
            Weight = weight;
            Name = name;
        }

        public int CompareTo(Item other)
        {
            return other == null ? 1 : GetComparatorValue().CompareTo(other.GetComparatorValue());
        }

        protected virtual double GetComparatorValue()
        {
            return Weight;
        }
    }
}
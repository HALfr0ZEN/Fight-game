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

        /// <summary>
        /// Implementation of IComparable
        /// </summary>
        /// <param name="other">Other item</param>
        /// <returns>1 if other is null else CompareTo value</returns>
        public int CompareTo(Item other) =>
            other == null ? 1 : GetComparatorValue().CompareTo(other.GetComparatorValue());

        /// <summary>
        /// Abstract method which should be implemented in all of child classes
        /// </summary>
        /// <returns>Double precision value</returns>
        protected abstract double GetComparatorValue();
    }
}
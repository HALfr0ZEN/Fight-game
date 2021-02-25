﻿namespace Hero_and_Dragon.Items
{
    public class Item
    {
        protected int Weight { get; }
        protected string Name { get; }

        public Item(int weight, string name)
        {
            Weight = weight;
            Name = name;
        }
    }
}
namespace Hero_and_Dragon.Items
{
    public class DefensiveItem : Item
    {
        public int Defense { get; }

        public DefensiveItem(int defense, int weight, string name) : base(weight, name)
        {
            Defense = defense;
        }
    }
}
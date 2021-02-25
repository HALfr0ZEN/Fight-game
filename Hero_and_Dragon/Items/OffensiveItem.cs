namespace Hero_and_Dragon.Items
{
    public class OffensiveItem : Item
    {
        public int Damage { get; }

        public OffensiveItem(int damage, int weight, string name) : base(weight, name)
        {
            Damage = damage;
        }
    }
}
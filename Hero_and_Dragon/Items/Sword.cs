namespace Hero_and_Dragon.Items
{
    /// <summary>
    /// Type of an offensive item
    /// </summary>
    public class Sword : Offensive
    {
        public Sword(int damage, int weight, string name) : base(weight, name, damage)
        {
        }
    }
}
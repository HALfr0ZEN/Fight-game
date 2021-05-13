namespace Hero_and_Dragon.Items
{
    /// <summary>
    /// Type of a defensive item
    /// </summary>
    public class Shield : Defensive
    {
        public Shield(int defense, int weight, string name) : base(weight, name, defense)
        {
        }
    }
}
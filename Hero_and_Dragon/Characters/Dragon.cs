using Hero_and_Dragon.Enums;

namespace Hero_and_Dragon.Characters
{
    /// <summary>
    /// Dragon is a character that has no different methods or no new implementation
    /// </summary>
    /// <seealso cref="Character"/>
    internal class Dragon : Character
    {
        /// <summary>
        /// Constructor for Dragon and the base Character
        /// Dragon belongs to a fraction Dragons
        /// </summary>
        /// <param name="name">Name of the dragon</param>
        /// <param name="health">Health that is equal to max health</param>
        /// <param name="damage">MaxDamage that dragon can give to others</param>
        /// <param name="defense">MaxDefense that dragon can provide to yourself</param>
        public Dragon(string name, int health, int damage, int defense) : base(name, health, damage, defense,
            Fractions.Dragons)
        {
        }
    }
}
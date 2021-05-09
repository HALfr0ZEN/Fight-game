using Hero_and_Dragon.Enums;

namespace Hero_and_Dragon.Characters
{
    /*
     * Dragon is a special type of a character
     * He belongs to fraction dragons.
     */
    class Dragon : Character
    {
        public Dragon(string name, int health, int damage, int defense) : base(name, health, damage, defense, Fractions.Dragons)
        {
        }
        
    }
}
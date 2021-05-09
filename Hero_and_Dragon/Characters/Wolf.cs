using System.Collections.Generic;
using System.Linq;
using Hero_and_Dragon.Enums;

namespace Hero_and_Dragon.Characters
{
    /*
     * Wolf is a special type of character which fights in a group of wolfs
     * He belongs to fraction animals.
     */
    class Wolf : Character
    {
        public Wolf(string name, int health, int damage, int defense) : base(name, health, damage, defense, Fractions.Animals)
        {
        }
        public override void SelectOpponent(List<Character> characters)
        {
            List<Character> opponents = characters.Where(character => character.IsAlive() && character.GetType() != GetType()).ToList();
            Opponent =  opponents.Count > 0 ? opponents[Dice.Instance.Throw(0, opponents.Count)] : null;
        }
    }
}
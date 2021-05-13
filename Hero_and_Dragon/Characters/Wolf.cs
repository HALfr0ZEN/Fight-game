using System.Collections.Generic;
using System.Linq;
using Hero_and_Dragon.Enums;

namespace Hero_and_Dragon.Characters
{
    /// <summary>
    /// Wolf is a special type of character which fights with everyone else but wolfs
    /// He belongs to fraction animals but it doesn't matter
    /// </summary>
    class Wolf : Character
    {
        /// <summary>
        /// Constructor for the Wolf and the base Character
        /// Wolf belongs to a fraction of Animals
        /// </summary>
        /// <param name="name">Name of the wolf</param>
        /// <param name="health">Health that is equal to max health</param>
        /// <param name="damage">MaxDamage that wolf can give to others</param>
        /// <param name="defense">MaxDefense that wolf can provide to yourself</param>
        public Wolf(string name, int health, int damage, int defense) : base(name, health, damage, defense,
            Fractions.Animals)
        {
        }

        /// <summary>
        /// Select next opponent from character given and then save it into property
        /// </summary>
        /// <param name="characters">Characters in which will be looked after new opponent(or same)</param>
        public override void SelectOpponent(List<Character> characters)
        {
            List<Character> opponents = characters
                .Where(character => character.IsAlive() && character.GetType() != GetType()).ToList();
            Opponent = opponents.Count > 0 ? opponents[Dice.Instance.Throw(0, opponents.Count)] : null;
        }
    }
}
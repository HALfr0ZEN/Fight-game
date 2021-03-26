using System.Collections.Generic;
using Hero_and_Dragon.Enums;

namespace Hero_and_Dragon.Characters
{
    /*
     * Wolf is a special type of character which fights in a group of wolfs
     * He belongs to fraction animals.
     */
    class Wolf : Character
    {
        public Wolf(string name, int health, int maxDamage, int maxDefense) : base(name, health, maxDamage, maxDefense, Fractions.Animals)
        {
        }
        public override Character SelectOpponent(List<Character> characters)
        {
            List<Character> opponents = new List<Character>();

            foreach (var character in characters)
            {
                if (character.IsAlive() && character.GetType() != GetType())
                {
                    opponents.Add(character);
                }
            }
            return opponents.Count > 0 ? opponents[Generating.Next(0, opponents.Count)] : null;
        }
    }
}
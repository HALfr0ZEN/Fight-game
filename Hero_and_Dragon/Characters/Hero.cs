using System.Collections.Generic;
using Hero_and_Dragon.Enums;
using Hero_and_Dragon.Items;

namespace Hero_and_Dragon.Characters
{
    /*
     * Hero is a special type a Warrior
     * He can fight only with other fractions (because he is heroic and not evil to fight with people)
     */
    class Hero : Warrior
    {
        public Hero(string name, int health, int maxDamage, int maxDefense, List<Item> items) : base(name, health, maxDamage, maxDefense, items)
        {
        }

        public Hero(string name, int health, int maxDamage, int maxDefense, OffensiveItem offensiveItem) : base(name, health, maxDamage, maxDefense, offensiveItem)
        {
        }

        public Hero(string name, int health, int maxDamage, int maxDefense, DefensiveItem defensiveItem) : base(name, health, maxDamage, maxDefense, defensiveItem)
        {
        }

        public Hero(string name, int health, int maxDamage, int maxDefense) : base(name, health, maxDamage, maxDefense)
        {
        }
        
        public override Character SelectOpponent(List<Character> characters)
        {
            List<Character> opponents = new List<Character>();
            foreach (var character in characters)
            {
                if (character.IsAlive() && character.Fraction != Fractions.People)
                {
                    opponents.Add(character);
                }
            }

            return opponents.Count > 0 ? opponents[Generating.Next(0, opponents.Count)] : null;
        }
    }
}
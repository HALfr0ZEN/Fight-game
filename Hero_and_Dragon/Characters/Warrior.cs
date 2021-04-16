using System.Collections.Generic;
using System.Linq;
using Hero_and_Dragon.Items;
using Hero_and_Dragon.Enums;

namespace Hero_and_Dragon.Characters
{
    /*
     * Warrior is a special type a character that can use Items.
     * He can have OffensiveItem/s and/or DefensiveItem/s.
     * He belongs to fraction people.
     */
    class Warrior : Character
    {
        private readonly List<Offensive> _offensiveItem = new List<Offensive>();
        private readonly List<Defensive> _defensiveItem = new List<Defensive>();
        
        public Warrior(string name, int health, int maxDamage,
            int maxDefense, List<Item> items) : base(name, health, maxDamage, maxDefense, Fractions.People)
        {
            foreach (var item in items)
            {
                switch (item)
                {
                    /*item AS DefensiveItem = defense*/
                    case Defensive defense:
                        _defensiveItem.Add(defense);
                        break;
                    /*item AS offensiveItem = offense*/
                    case Offensive offense:
                        _offensiveItem.Add(offense);
                        break;
                }
            }
        }

        public Warrior(string name, int health, int maxDamage, int maxDefense, Offensive offensiveItem) : base(name,
            health, maxDamage, maxDefense, Fractions.People)
        {
            _offensiveItem.Add(offensiveItem);
        }

        public Warrior(string name, int health, int maxDamage, int maxDefense, Defensive defensiveItem) : base(name,
            health, maxDamage, maxDefense, Fractions.People)
        {
            _defensiveItem.Add(defensiveItem);
        }

        public Warrior(string name, int health, int maxDamage, int maxDefense) : base(name, health, maxDamage, maxDefense, Fractions.People)
        {
        }

        public override int Attack(Character enemy)
        {
            int defense = enemy.Defense();

            // if weapon is null than return 0 else return dmg
            int chosenWeapon = _offensiveItem.Count > 0
                ? _offensiveItem[Generating.Next(0, _offensiveItem.Count - 1)].Damage
                : 0;
            // generate random dmg from 0 to maxDmg + weapon dmg
            int damage = Generating.Next(0, MaxDamage + chosenWeapon);

            // If damage is grater than defense than Health = Health - (damage - defense) 
            if (damage > defense)
                enemy.Health -= damage - defense;

            return damage;
        }

        public override int Defense()
        {
            // if shield or armor is null than return 0 else return def
            int chosenDefense = _defensiveItem.Count > 0
                ? _defensiveItem[Generating.Next(0, _defensiveItem.Count - 1)].Defense
                : 0;

            return Generating.NextDouble() <= 0.5 ? Generating.Next(0, MaxDefense + chosenDefense) : 0;
        }
        
        public override double GetStrength()
        {
            double outa = 0.3 * Health + 0.4 * (MaxDamage + _offensiveItem.Max()?.Damage ?? 0) + 0.3 * (MaxDefense + _defensiveItem.Max()?.Defense ?? 0);
            return outa;
        }
    }
}
using System.Collections.Generic;
using Hero_and_Dragon.Items;

namespace Hero_and_Dragon.Characters
{
    class Hero : Character
    {
        private readonly List<OffensiveItem> _offensiveItem = new List<OffensiveItem>();
        private readonly List<DefensiveItem> _defensiveItem = new List<DefensiveItem>();

        public Hero(string name, int health, int maxDamage,
            int maxDefense, List<Item> items) : base(name, health, maxDamage, maxDefense)
        {
            foreach (var item in items)
            {
                switch (item)
                {
                    /*item AS DefensiveItem = defense*/
                    case DefensiveItem defense:
                        _defensiveItem.Add(defense);
                        break;
                    /*item AS offensiveItem = offense*/
                    case OffensiveItem offense:
                        _offensiveItem.Add(offense);
                        break;
                }
            }
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
    }
}
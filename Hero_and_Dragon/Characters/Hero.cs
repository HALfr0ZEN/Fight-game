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
    class Hero : Character
    {
        private readonly List<Offensive> _offensiveItem = new List<Offensive>();
        private readonly List<Defensive> _defensiveItem = new List<Defensive>();
        
        
        public Hero(string name, int health, int damage,
            int defense, List<Item> items) : base(name, health, damage, defense, Fractions.People)
        {
            foreach (var item in items)
            {
                switch (item)
                {
                    /*item AS DefensiveItem = defense*/
                    case Defensive defensive:
                        _defensiveItem.Add(defensive);
                        break;
                    /*item AS offensiveItem = offense*/
                    case Offensive offensive:
                        _offensiveItem.Add(offensive);
                        break;
                }
            }
        }

        public Hero(string name, int health, int damage, int defense, Offensive offensiveItem) : base(name,
            health, damage, defense, Fractions.People)
        {
            _offensiveItem.Add(offensiveItem);
        }

        public Hero(string name, int health, int damage, int defense, Defensive defensiveItem) : base(name,
            health, damage, defense, Fractions.People)
        {
            _defensiveItem.Add(defensiveItem);
        }

        public Hero(string name, int health, int damage, int defense) : base(name, health, damage, defense, Fractions.People)
        {
        }

        public override int Attack(Character enemy)
        {
            int defense = enemy.Defend();

            // if weapon is null than return 0 else return dmg
            int chosenWeapon = _offensiveItem.Count > 0
                ? _offensiveItem[Dice.Instance.Throw(0, _offensiveItem.Count - 1)].Damage
                : 0;
            // generate random dmg from 0 to maxDmg + weapon dmg
            int damage = Dice.Instance.Throw(0, Damage + chosenWeapon);

            // If damage is grater than defense than Health = Health - (damage - defense) 
            enemy.PrevHealth = enemy.Health;
            
            if (damage > defense)
                enemy.Health -= damage - defense;
            return damage;
        }

        public override int Defend()
        {
            // if shield or armor is null than return 0 else return def
            int chosenDefense = _defensiveItem.Count > 0
                ? _defensiveItem[Dice.Instance.Throw(0, _defensiveItem.Count - 1)].Defense
                : 0;
            
            return Dice.Instance.Throw() <= 0.5 ? Dice.Instance.Throw(0, Defense + chosenDefense) : 0;
        }
        public override void SelectOpponent(List<Character> characters)
        {
            List<Character> opponents = characters.Where(character => character.IsAlive() && character.Fraction != Fractions.People).ToList();

            Opponent = opponents.Count > 0 ? opponents[Dice.Instance.Throw(0, opponents.Count)] : null;
        }
        
        public override double GetStrength() =>  0.3 * Health + 0.4 * GetMaxDamage() + 0.3 * GetMaxDefense();

        public override int GetMaxDamage() => Damage + (_offensiveItem.Max()?.Damage ?? 0);
        public override int GetMaxDefense() => Defense + (_defensiveItem.Max()?.Defense ?? 0);
    }
}
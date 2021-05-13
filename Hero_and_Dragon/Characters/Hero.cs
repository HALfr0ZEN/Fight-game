using System.Collections.Generic;
using System.Linq;
using Hero_and_Dragon.Items;
using Hero_and_Dragon.Enums;

namespace Hero_and_Dragon.Characters
{
    /// <summary>
    /// Hero is a special type of a character that can use Items
    /// He can have OffensiveItem/s and/or DefensiveItem/s
    /// He belongs to fraction people
    /// </summary>
    /// <seealso cref="Character"/>
    internal class Hero : Character
    {
        private readonly List<Offensive> _offensiveItems = new List<Offensive>();
        private readonly List<Defensive> _defensiveItems = new List<Defensive>();

        /// <summary>
        /// This ctor defines (besides of base data) which item is offensive and which not
        /// </summary>
        /// <param name="name">Name of the hero</param>
        /// <param name="health">Health that is equal to max health</param>
        /// <param name="damage">MaxDamage that hero can give to others</param>
        /// <param name="defense">MaxDefense that hero can provide to yourself</param>
        /// <param name="items">List of items which will be divided in to two lists of corresponding types</param>
        public Hero(string name, int health, int damage,
            int defense, List<Item> items) : base(name, health, damage, defense, Fractions.People)
        {
            foreach (var item in items)
            {
                switch (item)
                {
                    /*item AS DefensiveItem = defense*/
                    case Defensive defensive:
                        _defensiveItems.Add(defensive);
                        break;
                    /*item AS offensiveItem = offense*/
                    case Offensive offensive:
                        _offensiveItems.Add(offensive);
                        break;
                }
            }
        }

        /// <summary>
        /// This ctor adding (besides of base data) provided offensiveItem to corresponding list
        /// </summary>
        /// <param name="name">Name of the hero</param>
        /// <param name="health">Health that is equal to max health</param>
        /// <param name="damage">MaxDamage that hero can give to others</param>
        /// <param name="defense">MaxDefense that hero can provide to yourself</param>
        /// <param name="offensiveItems">One or more offensive items which will be added to corresponding list</param>
        public Hero(string name, int health, int damage, int defense, params Offensive[] offensiveItems) : base(name,
            health, damage, defense, Fractions.People)
        {
            _offensiveItems.AddRange(offensiveItems);
        }

        /// <summary>
        /// This ctor adding (besides of base data) provided defensiveItem to corresponding list
        /// </summary>
        /// <param name="name">Name of the hero</param>
        /// <param name="health">Health that is equal to max health</param>
        /// <param name="damage">MaxDamage that hero can give to others</param>
        /// <param name="defense">MaxDefense that hero can provide to yourself</param>
        /// <param name="defensiveItems">One or more defensive items which will be added to corresponding list</param>
        public Hero(string name, int health, int damage, int defense, params Defensive[] defensiveItems) : base(name,
            health, damage, defense, Fractions.People)
        {
            _defensiveItems.AddRange(defensiveItems);
        }

        /// <summary>
        /// This ctor adding (besides of base data) provided offensiveItem to corresponding list
        /// </summary>
        /// <param name="name">Name of the hero</param>
        /// <param name="health">Health that is equal to max health</param>
        /// <param name="damage">MaxDamage that hero can give to others</param>
        /// <param name="defense">MaxDefense that hero can provide to yourself</param>
        public Hero(string name, int health, int damage, int defense) : base(name, health, damage, defense,
            Fractions.People)
        {
        }

        /// <summary>
        /// If enemy is given than character will attack with a random damage calculated from max damage + randomly chosen weapon and eventually
        /// enemy's health will be decremented 
        /// </summary>
        /// <param name="enemy">Character type object whose health will be decremented</param>
        /// <returns>Damage given to enemy</returns>
        public override int Attack(Character enemy)
        {
            int defense = enemy.Defend();

            // if count weapons is 0 (or somehow less) then return 0 else return dmg
            int chosenWeapon = _offensiveItems.Count > 0
                ? _offensiveItems[Dice.Instance.Throw(0, _offensiveItems.Count - 1)].Damage
                : 0;
            // generate random dmg from 0 to maxDmg + weapon dmg
            int damage = Dice.Instance.Throw(0, Damage + chosenWeapon);

            //save prev health of enemy to stating enemy
            enemy.PrevHealth = enemy.Health;
            // If damage is grater than defense than Health = Health - (damage - defense) 
            if (damage > defense)
                enemy.Health -= damage - defense;
            return damage;
        }

        /// <summary>
        /// Try to defend yourself by throwing the dice and calculating the random value from max defense with chosen shield
        /// </summary>
        /// <returns>Provided defense as integer or 0 if none</returns>
        public override int Defend()
        {
            // if shield or armor is null than return 0 else return def
            int chosenDefense = _defensiveItems.Count > 0
                ? _defensiveItems[Dice.Instance.Throw(0, _defensiveItems.Count - 1)].Defense
                : 0;

            return Dice.Instance.Throw() <= 0.5 ? Dice.Instance.Throw(0, Defense + chosenDefense) : 0;
        }

        /// <summary>
        /// Select next opponent from character given and then save it into property
        /// </summary>
        /// <param name="characters">Characters in which will be looked after new opponent(or same)</param>
        public override void SelectOpponent(List<Character> characters)
        {
            List<Character> opponents = characters
                .Where(character => character.IsAlive() && character.Fraction != Fractions.People).ToList();

            Opponent = opponents.Count > 0 ? opponents[Dice.Instance.Throw(0, opponents.Count)] : null;
        }

        /// <summary>
        /// Get max defense that hero can provide
        /// Calculated from max defensiveItem stat and default defense
        /// </summary>
        /// <returns>Max defense capable of</returns>
        public override int GetMaxDefense() => Defense + (_defensiveItems.Max()?.Defense ?? 0);

        /// <summary>
        /// Get max damage that hero can provide
        /// Calculated from max damageItem stat and default damage
        /// </summary>
        /// <returns>Max damage capable of</returns>
        public override int GetMaxDamage() => Damage + (_offensiveItems.Max()?.Damage ?? 0);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Hero_and_Dragon.Enums;

namespace Hero_and_Dragon.Characters
{
    /*
     *
     * Character is a abstract class that describes base of all characters in the game
     * He defines that everyone without overridden SelectOpponent can kill everyone (except themself)
     * Every character have a name, health, maximal damage, maximal defense, randomGenerated number and belongs to one of the fractions
     * Every character can attack and defense and can be checked if still alive
     */
    internal abstract class Character : IComparable<Character>
    {
        public string Name { get; }
        private int _health;

        public int Health
        {
            get => _health;
            set => _health = value < 0 ? 0 : value; // set to zero if damage is > health
        }

        public int PrevHealth;

        protected int MaxHealth;

        private bool Escaped { get; set; }
        private int EscapeTime { get; set; }
        protected int MaxDamage { get; }
        protected int MaxDefense { get; }
        
        protected internal Fractions Fraction{ get; }
        protected Character(string name, int health, int maxDamage, int maxDefense, Fractions fraction)
        {
            Name = name;
            Health = health;
            PrevHealth = Health;
            MaxHealth = health;
            MaxDamage = maxDamage;
            MaxDefense = maxDefense;
            Fraction = fraction;
        }
        
        public virtual int Attack(Character enemy)
        {
            int defense = enemy.Defense();

            int damage = Dice.Instance.Throw(0, MaxDamage);

            enemy.PrevHealth = enemy.Health; 
            //patch negative numbers 
            if (damage > defense)
                enemy.Health -= damage - defense;

            return damage;
        }
        
        public virtual int Defense()
        {
            return Dice.Instance.Throw() <= 0.5 ? Dice.Instance.Throw(0, MaxDefense) : 0;
        }
        
        public bool IsAlive()
        {
            return Health > 0;
        }
        
        public virtual Character SelectOpponent(List<Character> characters)
        {
            List<Character> opponents = characters.Where(character => character.IsAlive() && character != this).ToList();

            return opponents.Count > 0 ? opponents[Dice.Instance.Throw(0, opponents.Count)] : null;
        }
        
        public EscapeEnum Escape()
        {
            if (Health >= MaxHealth * 0.5)
                return EscapeEnum.DontHaveValues;
            
            if (EscapeTime == 0)
            {
                if (!Escaped && Dice.Instance.Throw() <= 0.2)
                {
                    Escaped = true;
                    Health = 0;
                    return EscapeEnum.Escaped;
                }
                EscapeTime = 2; /*time that character need to wait before new try*/
                return EscapeEnum.Tried;
            }
            --EscapeTime;
            return EscapeEnum.DontHaveValues;
        }

        public int CompareTo(Character other)
        {
            return other == null ? 1 : GetStrength().CompareTo(other.GetStrength());
        }

        public virtual double GetStrength()
        {
            return 0.3 * Health + 0.4 * MaxDamage + 0.3 * MaxDefense;
        }
    }
}
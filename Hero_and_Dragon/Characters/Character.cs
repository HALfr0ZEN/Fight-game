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
        public event Action<Character, Character> OpponentChange;
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
        protected int Damage { get; }
        protected int Defense { get; }
        
        private Character _opponent;

        public Character Opponent
        {
            get => _opponent;
            protected set
            {
                if (_opponent == value) return;
                _opponent = value;
                OpponentChange?.Invoke(this, _opponent);
            }
        }

        protected internal Fractions Fraction { get; }

        protected Character(string name, int health, int damage, int defense, Fractions fraction)
        {
            Name = name;
            Health = health;
            PrevHealth = Health;
            MaxHealth = health;
            Damage = damage;
            Defense = defense;
            Fraction = fraction;
        }

        public virtual int Attack(Character enemy)
        {
            int defense = enemy.Defend();

            int damage = Dice.Instance.Throw(0, Damage);

            enemy.PrevHealth = enemy.Health;
            //patch negative numbers 
            if (damage > defense)
                enemy.Health -= damage - defense;

            return damage;
        }

        public virtual int Defend() => Dice.Instance.Throw() <= 0.5 ? Dice.Instance.Throw(0, Defense) : 0;

        public bool IsAlive() => Health > 0;

        public int CompareTo(Character other) => other == null ? 1 : GetStrength().CompareTo(other.GetStrength());

        public virtual double GetStrength() => 0.3 * Health + 0.4 * Damage + 0.3 * Defense;

        public virtual int GetMaxDefense() => Defense;

        public virtual int GetMaxDamage() => Damage;

        public virtual void SelectOpponent(List<Character> characters)
        {
            List<Character> opponents =
                characters.Where(character => character.IsAlive() && character != this).ToList();
            
            Opponent = opponents.Count > 0 ? opponents[Dice.Instance.Throw(0, opponents.Count)] : null;
        }
        

        public EscapeEnum Escape()
        {
            if (Health >= MaxHealth * 0.5)
                return EscapeEnum.Cant;

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
            return EscapeEnum.Cant;
        }
    }
}
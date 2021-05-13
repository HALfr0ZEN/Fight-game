using System;
using System.Collections.Generic;
using System.Linq;
using Hero_and_Dragon.Enums;

namespace Hero_and_Dragon.Characters
{
    /// <summary>
    /// Character is an abstract class that describes basic behaviour of all characters in the game.
    /// Every character have a name, health (previous health), max damage deal to others, max defense provide to yourself
    /// </summary>
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
        protected readonly int MaxHealth;

        private bool Escaped { get; set; }
        private int EscapeTime { get; set; }
        protected int Damage { get; }
        protected int Defense { get; }

        private Character _opponent;

        /// <summary>
        /// Get for this property will return _opponent value
        /// Set for this property will set _opponent to value and invoke Action event or return if value is the same as _opponent
        /// </summary>
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

        /// <summary>
        /// Basic base constructor
        /// </summary>
        /// <param name="name">Name of a character</param>
        /// <param name="health">Health that is equal to max health</param>
        /// <param name="damage">MaxDamage that character can give to others</param>
        /// <param name="defense">MaxDefense that character can provide to yourself</param>
        /// <param name="fraction">Fraction where character belongs</param>
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

        /// <summary>
        /// If enemy is given than character will attack with a random damage calculated from max damage and eventually
        /// enemy's health will be decremented 
        /// </summary>
        /// <param name="enemy">Character type object whose health will be decremented</param>
        /// <returns>Damage given to enemy</returns>
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

        /// <summary>
        /// Try to defend yourself by throwing the dice and calculating the random value from max defense
        /// </summary>
        /// <returns>Provided defense as integer or 0 if none</returns>
        public virtual int Defend() => Dice.Instance.Throw() <= 0.5 ? Dice.Instance.Throw(0, Defense) : 0;

        /// <summary>
        /// Checks if character is still alive by comparing health with a 0
        /// </summary>
        /// <returns>True if health is greater than 0 (character is alive) else false (character is dead)</returns>
        public bool IsAlive() => Health > 0;

        /// <summary>
        /// Implementation of IComparable interface method.
        /// </summary>
        /// <param name="other">Character type object that will be compared to this</param>
        /// <returns>Highest of comparison of strengths of both characters (this, other)</returns>
        public int CompareTo(Character other) => other == null ? 1 : GetStrength().CompareTo(other.GetStrength());

        /// <summary>
        /// Gets strength from evenly distributed average (.3 health, .4 damage, .3 defense)
        /// </summary>
        /// <returns>strength of character</returns>
        public double GetStrength() => 0.3 * Health + 0.4 * GetMaxDamage() + 0.3 * GetMaxDefense();

        /// <summary>
        /// Gets max defense that character can provide
        /// </summary>
        /// <returns>Max defense capable of</returns>
        public virtual int GetMaxDefense() => Defense;

        /// <summary>
        /// Gets max damage that character can provide
        /// </summary>
        /// <returns>Max damage capable of</returns>
        public virtual int GetMaxDamage() => Damage;

        /// <summary>
        /// Select next opponent from characters and save next opponent to property (actually to field) 
        /// </summary>
        /// <param name="characters">Characters in which will be looked after new opponent(or same)</param>
        public virtual void SelectOpponent(List<Character> characters)
        {
            List<Character> opponents =
                characters.Where(character => character.IsAlive() && character != this).ToList();

            Opponent = opponents.Count > 0 ? opponents[Dice.Instance.Throw(0, opponents.Count)] : null;
        }

        /// <summary>
        /// Calculation of escape time and randomizing it´s probability
        /// </summary>
        /// <returns>Enumerator value which corresponds to escaped, tried to escape and can not escape</returns>
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

        /// <summary>
        /// Find all characters that can fight (are alive and are not the same fraction as this)
        /// </summary>
        /// <param name="characters"></param>
        /// <returns>if number of characters that can fight is grater than 1 than true else false</returns>
        public static bool CanFight(List<Character> characters)
        {
            List<int> countAlive = new List<int>();

            for (int i = 0; i < 4; i++)
                countAlive.Add(characters.FindAll(character => (int) character.Fraction == i && character.IsAlive())
                    .Count > 0
                    ? 1
                    : 0);

            return countAlive.Sum() > 1;
        }
    }
}
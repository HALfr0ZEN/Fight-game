using System;

namespace Hero_and_Dragon.Characters
{
    class Hero
    {
        public string Name { get; }

        private int _health;

        public int Health
        {
            get => _health;
            set => _health = value < 0 ? 0 : value; // set to zero if damage is > health
        }

        public int MaxDamage { get; }
        public int MaxDefense { get; }

        private readonly Random _generating = new Random();

        public Hero(string name, int health, int maxDamage, int maxDefense)
        {
            this.Name = name;
            this.Health = health;
            this.MaxDamage = maxDamage;
            this.MaxDefense = maxDefense;
        }

        /*Character attack*/
        public (int damage, int enemyDefense) Attack(Dragon enemy)
        {
            int defense = enemy.Defense();

            int damage = Convert.ToInt32(_generating.NextDouble() * MaxDamage);

            //patch negative numbers 
            if (damage > defense)
                enemy.Health -= damage - defense;

            return (damage, defense);
        }

        public int Defense()
        {
            return Math.Round(_generating.NextDouble()) == 1 ? _generating.Next(0, MaxDefense) : 0;
        }

        /*Check if character is alive*/
        public bool IsAlive()
        {
            return Health > 0;
        }
    }
}
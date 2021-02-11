using System;

namespace Hero_and_Dragon.Characters
{
    class Dragon
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxDamage { get; set; }
        public int MaxDefense { get; set; }
        
        private readonly Random _generating = new Random();

        public Dragon(string name, int health, int maxDamage, int maxDefense)
        {
            this.Name = name;
            this.Health = health;
            this.MaxDamage = maxDamage;
            this.MaxDefense = maxDefense;
        }
        
        /*Character attack*/
        public int Attack(Hero hero)
        {
            int damage = Convert.ToInt32(_generating.NextDouble() * MaxDamage);
            int defense = hero.Defense();
            damage -= defense;
            hero.Health -= damage;

            return damage;
        }
        
        /*Random character defense*/ 
        public int Defense()
        {   
            int defense = 0;
            if (Math.Round(_generating.NextDouble()) == 1)
            {
                defense = _generating.Next(0, MaxDefense);
            }

            return defense;
        }
        
        /*Check if character is alive*/
        public bool IsAlive()
        {
            return Health > 0;
        }
    }
}
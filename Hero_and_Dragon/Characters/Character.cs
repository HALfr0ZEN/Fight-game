﻿using System;
using System.Collections.Generic;

namespace Hero_and_Dragon.Characters
{
    class Character
    {
        public string Name { get; }
        private int _health;

        public int Health
        {
            get => _health;
            set => _health = value < 0 ? 0 : value; // set to zero if damage is > health
        }

        protected int MaxDamage { get; }
        protected int MaxDefense { get; }

        protected readonly Random Generating = new Random();

        protected Character(string name, int health, int maxDamage, int maxDefense)
        {
            this.Name = name;
            this.Health = health;
            this.MaxDamage = maxDamage;
            this.MaxDefense = maxDefense;
        }

        /*
         *  Character attack
         */
        public virtual int Attack(Character enemy)
        {
            int defense = enemy.Defense();

            int damage = Generating.Next(0, MaxDamage);

            //patch negative numbers 
            if (damage > defense)
                enemy.Health -= damage - defense;

            return damage;
        }

        /*
         *  Random character defense
         */
        public virtual int Defense()
        {
            return Generating.NextDouble() <= 0.5 ? Generating.Next(0, MaxDefense) : 0;
        }

        /*
         *  Check if character is alive
         */
        public bool IsAlive()
        {
            return Health > 0;
        }

        /*
         *  Select opponents from given list
         *  opponents are objects with different type than called
         */
        public Character SelectOpponent(List<Character> characters)
        {
            List<Character> opponents = new List<Character>();

            //Loop thru all players (characters)
            foreach (var character in characters)
            {
                // check if selected opponent is not ally and if its alive
                // if true add him into opponents list
                if (character.IsAlive() && character.GetType() != GetType())
                {
                    opponents.Add(character);
                }
            }

            // pick one random opponent and return it
            return opponents.Count > 0 ? opponents[Generating.Next(0, opponents.Count)] : null;
        }
    }
}
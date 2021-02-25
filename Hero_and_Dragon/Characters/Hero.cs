using System;
using System.Collections.Generic;
using Hero_and_Dragon.Items;
using Microsoft.VisualBasic.CompilerServices;

namespace Hero_and_Dragon.Characters
{
    /*
     Hrdina bude mít navíc předměty; konkrétně meč a štít. Tyto předměty budou vytvořeny v samostatné třídě,
     a kromě hodnot velikosti poškození (meč) a obrany (štít) budou mít také název a váhu (tzn., že mají společná data). 
     Hrdina je poté bude používat (zde bude provedena kompozice). Meč a štít budou aplikovány u hrdiny v metodě pro útok,
     resp. pro obranu (tzn. že se do výpočtu budou započítávat hodnoty vybavení). Hrdina tak bude mít jiný výpočet útoku a obrany,
     než jaký má drak.
    */
    class Hero : Character
    {
        private readonly OffensiveItem _offensiveItem;
        private readonly DefensiveItem _defensiveItem;
        public Hero(OffensiveItem offensiveItem, DefensiveItem defensiveItem, string name, int health, int maxDamage, int maxDefense) : base(name, health, maxDamage, maxDefense)
        {
            _offensiveItem = offensiveItem;
            _defensiveItem = defensiveItem;
        }
        public override int Attack(Character enemy)
        {
            int defense = enemy.Defense();

            int damage = generating.Next(0, MaxDamage + (_offensiveItem?.Damage ?? 0)); //pokud je null tak 0

            //patch negative numbers 
            if (damage > defense)
                enemy.Health -= damage - defense;

            return damage;
        }
        
        public override int Defense()
        {
            return generating.NextDouble() <= 0.5 ? generating.Next(0, MaxDefense + (_defensiveItem?.Defense ?? 0)) : 0;
        }
    }
}
using System;

namespace Hero_and_Dragon.Characters
{
    class Hero : Character
    {
        /*Calling constructor of the base class (bcs !parameter-less)*/
        public Hero(string name, int health, int maxDamage, int maxDefense) : base(name, health, maxDamage, maxDefense)
        {
        }
    }
}
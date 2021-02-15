using System;

namespace Hero_and_Dragon.Characters
{
    class Dragon:Character
    {
        /*Calling constructor of the base class (bcs !parameter-less)*/
        public Dragon(string name, int health, int maxDamage, int maxDefense) : base(name,  health, maxDamage,  maxDefense)
        {}
    }
}
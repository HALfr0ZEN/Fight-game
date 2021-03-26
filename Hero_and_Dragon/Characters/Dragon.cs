using System;
using System.Collections.Generic;
using Hero_and_Dragon.Enums;

namespace Hero_and_Dragon.Characters
{
    /*
     * Dragon is a special type of a character
     * He belongs to fraction dragons.
     */
    class Dragon : Character
    {
       
        public Dragon(string name, int health, int maxDamage, int maxDefense) : base(name, health, maxDamage, maxDefense, Fractions.Dragons)
        {
        }
        
    }
}
using System;
using Hero_and_Dragon.Characters;

namespace Hero_and_Dragon
{
    class Program
    {
        /*Ošetření záporných čísel -> záporné čísla u útoku, záporná čísla u zdraví, zabránění útoku draka po jeho smrti a naopak*/
        static void Main(string[] args)
        {
            /*fight in loop while one death*/
            Hero hero = new Hero(name: "Legolas", 500, 50, 40);
            Dragon dragon = new Dragon(name: "Parthumanax", 500, 35, 50);


            while (hero.IsAlive() && dragon.IsAlive())
            {
                var heroAttack = hero.Attack(dragon);
                Console.WriteLine($"Hero attack was {heroAttack.damage} because dragon defended with {heroAttack.defense}");
                
                var dragonAttack = dragon.Attack(hero);
                Console.WriteLine($"Dragon attack was {dragonAttack.damage} because hero defended with {dragonAttack.defense}");
                
                Console.WriteLine("Hero health is " + hero.Health);
                Console.WriteLine("Dragon health is " + dragon.Health);
                
                Console.WriteLine();
            } 

            if (hero.IsAlive())
            {
                Console.WriteLine("Hrdina vyhrál");
            }
            else if (dragon.IsAlive())
            {
                Console.WriteLine("Drak vyhrál");
            }
            else
            {
                Console.WriteLine("All died at the same time");
            }
        }
    }
}
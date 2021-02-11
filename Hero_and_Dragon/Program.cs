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
            Hero hero = new Hero(name: "Legolas", 500, 35, 40);
            Dragon dragon = new Dragon(name: "Parthumanax", 500, 35, 50);


            for (int i = 1; true ; i++)
            {
                Console.WriteLine("---------------");
                Console.WriteLine("Round: " + i);
                Console.WriteLine("---------------");

                (int damage, int enemyDefense) = hero.Attack(dragon);
                
                Console.WriteLine(hero.Name + " attack: " + damage);
                Console.WriteLine(dragon.Name +" defend: " + enemyDefense); 
                
                Console.WriteLine(dragon.Name+ " health: " + dragon.Health);
                if (!dragon.IsAlive())
                    break;
                
                Console.WriteLine();
                (damage, enemyDefense) = dragon.Attack(hero);
                
                Console.WriteLine(dragon.Name + " attack: " + damage);
                Console.WriteLine(hero.Name + " defend: " + enemyDefense);
                
                Console.WriteLine(hero.Name+ " health: " + hero.Health);
                if (!hero.IsAlive())
                    break;
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
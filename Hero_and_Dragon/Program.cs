using System;
using Hero_and_Dragon.Characters;

namespace Hero_and_Dragon
{
    class Program
    {
        static void Main(string[] args)
        {
            /*fight in loop while one death*/
            Hero hero = new Hero(name: "Legolas", 300, 50, 40);
            Dragon dragon = new Dragon(name: "Parthumanax", 400, 35, 50);
            

            for (int i = 0; dragon.IsAlive() && hero.IsAlive() ; i++)
            {
                Console.WriteLine("Hero attack: "+ hero.Attack(dragon));
                Console.WriteLine("Dragon attack: "+ dragon.Attack(hero));
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
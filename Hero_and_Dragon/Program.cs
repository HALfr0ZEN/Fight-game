using System;
using Hero_and_Dragon.Characters;

namespace Hero_and_Dragon
{
    class Program
    {
        static void Main(string[] args)
        {
            /*fight in loop while one death*/
            Hero hero = new Hero(name: "Geralt", 500, 35, 50);
            Dragon dragon = new Dragon(name: "Alduin", 500, 35, 40);


            for (int i = 1;; i++)
            {
                Console.WriteLine(Environment.NewLine + "--------------------");
                Console.WriteLine("Round: " + i);
                Console.WriteLine("--------------------");

                (int damage, int enemyDefense) = hero.Attack(dragon);

                Console.WriteLine(hero.Name + " attack: " + damage);
                Console.WriteLine(dragon.Name + " defend: " + enemyDefense);

                Console.WriteLine(dragon.Name + " health: " + dragon.Health);
                
                if (!dragon.IsAlive())
                {
                    Console.WriteLine(Environment.NewLine + hero.Name + " won!");
                    break;
                }

                Console.WriteLine();
                (damage, enemyDefense) = dragon.Attack(hero);

                Console.WriteLine(dragon.Name + " attack: " + damage);
                Console.WriteLine(hero.Name + " defend: " + enemyDefense);

                Console.WriteLine(hero.Name + " health: " + hero.Health);
                
                if (!hero.IsAlive())
                {
                    Console.WriteLine(Environment.NewLine + dragon.Name + " won!");
                    break;
                }
            }
        }
    }
}
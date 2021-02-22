using System;
using System.Collections.Generic;
using System.Threading.Channels;
using Hero_and_Dragon.Characters;

namespace Hero_and_Dragon
{
    class Program
    {
        static void Main(string[] args)
        {
            /*fight in loop while one death*/
            Hero hero = new Hero(name: "Geralt", 50, 60, 50);
            Hero hero2 = new Hero(name: "Dovakhin", 50, 60, 40);
            Dragon dragon = new Dragon(name: "Alduin", 60, 40, 80);
            Dragon dragon2 = new Dragon(name: "Šmak", 60, 40, 85);
            
            List<Character> characters = new List<Character>();
            
            characters.Add(hero);
            characters.Add(hero2);
            characters.Add(dragon);
            characters.Add(dragon2);

            for (int i = 1; HeroAliveCount(characters) > 0 && DragonAliveCount(characters) > 0 ; i++)
            {
                Console.WriteLine("Round: " + i);
                Console.WriteLine("===================");

                int j = 0;
                foreach (Character attacker in characters)
                {
                    
                    
                    if (attacker.IsAlive())
                    {   
                        Character opponent = attacker.SelectOpponent(characters);
                        
                        if (opponent == null)
                        {
                            break;
                        }
                        
                        ++j;
                        Console.WriteLine("Move: " +j + Environment.NewLine + "-------------------");
                        
                        int damage = attacker.Attack(opponent);
                        Console.WriteLine(attacker.Name + " attack with " + damage);
                        Console.WriteLine(opponent.Name + " health is now " + opponent.Health);
                        Console.WriteLine(Environment.NewLine);
                    }
                }
            }
            
            Console.WriteLine("Winners!"+Environment.NewLine+ "¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
            foreach (var character in characters)
            {
                if (character.IsAlive())
                {
                    Console.WriteLine(character.Name);
                }
            }
            Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
            
            //characters.ForEach(Character => Console.WriteLine(Character.IsAlive() ? Character.Name : null));
            
        }

        public static int HeroAliveCount(List<Character> characters)
        {
            int countAlive = 0;
            foreach (Character character in characters)
            {
                if (character is Hero && character.IsAlive())
                {
                        ++countAlive;
                }
            }

            return countAlive;
        }
        public static int DragonAliveCount(List<Character> characters)
        {
            int countAlive = 0;
            foreach (Character character in characters)
            {
                if (character is Dragon && character.IsAlive())
                {
                    ++countAlive;
                }
            }

            return countAlive;
        }
    }
}
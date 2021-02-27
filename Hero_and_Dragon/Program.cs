using System;
using System.Collections.Generic;
using System.Threading.Channels;
using Hero_and_Dragon.Characters;
using Hero_and_Dragon.Items;

namespace Hero_and_Dragon
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Item> geraltItems = new List<Item>();
            geraltItems.Add(new OffensiveItem(40, 2, "Silver sword"));
            
            List<Item> dovakhiinItems = new List<Item>();
            dovakhiinItems.Add(new OffensiveItem(40, 2, "Bow"));
            dovakhiinItems.Add(new DefensiveItem(40, 2, "shield"));

            /* Characters */
            Hero geralt = new Hero( "Geralt", 50, 15, 50, geraltItems);

            Hero dovakhiin = new Hero( "Dovakhiin", 50, 5, 20, dovakhiinItems);

            Dragon alduin = new Dragon("Alduin", 60, 40, 80);

            Dragon smaug = new Dragon("Smaug", 60, 40, 85);

            List<Character> characters = new List<Character>();

            characters.Add(geralt);
            characters.Add(dovakhiin);
            characters.Add(alduin);
            characters.Add(smaug);

            for (int i = 1; HeroAliveCount(characters) > 0 && DragonAliveCount(characters) > 0; i++)
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
                        Console.WriteLine("Move: " + j + Environment.NewLine + "-------------------");

                        int damage = attacker.Attack(opponent);
                        Console.WriteLine(attacker.Name + " attack with " + damage);
                        Console.WriteLine(opponent.Name + " health is now " + opponent.Health);
                        Console.WriteLine(Environment.NewLine);
                    }
                }
            }

            Console.WriteLine("Winners!" + Environment.NewLine + "¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
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
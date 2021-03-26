using System;
using System.Collections.Generic;
using System.Linq;
using Hero_and_Dragon.Characters;
using Hero_and_Dragon.Enums;
using Hero_and_Dragon.Items;

namespace Hero_and_Dragon
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Item> dovakhiinItems = new List<Item>()
            {
                new OffensiveItem(40, 2, "Bow"),
                new DefensiveItem(40, 2, "shield")
            };

            List<Character> characters = new List<Character>()
            {
                new Hero("Geralt", 50, 15, 50),
                new Warrior("Dovakhiin", 50, 5, 20, dovakhiinItems),
                new Dragon("Alduin", 60, 40, 80),
                new Dragon("Smaug", 60, 40, 85)
            };
            
            
            for (int i = 1; CanFight(characters); i++)
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
                            break;

                        ++j;
                        Console.WriteLine("Move: " + j + Environment.NewLine + "-------------------");

                        switch (attacker.Escape())
                        {
                            case (EscapeEnum.Escaped):
                            {
                                Console.WriteLine(attacker.Name + " escaped..."); 
                                continue;
                            }
                            case (EscapeEnum.Tried):
                            {
                                Console.WriteLine(attacker.Name + " tried to escape..."); 
                                continue;
                            }
                        }
                        
                        
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
        }
        
        // if more than 1 fraction is capable of fighting return true

        private static bool CanFight(List<Character> characters)
        {

            List<int> countAlive = new List<int>();
            
            for (int i = 0; i < 4; i++)
            {
                countAlive.Add(characters.FindAll(character => (int) character.Fraction == i && character.IsAlive()).Count > 0 ? 1 : 0);
            }

            return countAlive.Sum() > 1;
        }
    }
}
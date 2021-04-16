using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
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
                new Sword(10, 2, "MorningStar"),
                new Shield(40, 2, "MorningShield")
            };

            List<Character> characters = new List<Character>()
            {
                new Hero("Geralt", 50, 15, 50, new Sword(20, 2, "Silver sword")),
                new Warrior("Dovakhiin", 50, 5, 20, dovakhiinItems),
                new Dragon("Alduin", 60, 40, 80),
                new Dragon("Smaug", 60, 40, 85)
            };

            characters.Sort();
            characters.Reverse();
            Console.WriteLine($"{"CHARACTER".PadRight(10)}| STRENGTH");
            characters.ForEach(character => Console.WriteLine($"{character.Name.PadRight(10)}| {character.GetStrength()}"));
            Console.WriteLine(Environment.NewLine);
            
            
            
            for (int i = 1; CanFight(characters); i++)
            {
                Console.WriteLine("".PadRight(50,'-'));
                Console.WriteLine($"| ROUND: {i}".PadRight(49) + "|");
                Console.WriteLine("".PadRight(50,'-'));
                int j = 0;
                
                foreach (Character attacker in characters)
                {
                    if (!attacker.IsAlive()) continue;
                    
                    Character opponent = attacker.SelectOpponent(characters);

                    if (opponent == null)
                        break;

                    ++j;
                    
                    Console.WriteLine("-".PadRight(50,'-'));
                    Console.WriteLine($"| Move: {j}".PadRight(49) + "|");

                    switch (attacker.Escape())
                    {
                        case (EscapeEnum.Escaped):
                        {
                            Console.WriteLine($"| {attacker.Name} escaped...".PadRight(49) + "|");
                            Console.WriteLine("".PadRight(50,'-'));
                            
                            Console.WriteLine(Environment.NewLine);
                            continue;
                        }
                        case (EscapeEnum.Tried):
                        {
                            Console.WriteLine($"| {attacker.Name} tried to escape...".PadRight(49) + "|");
                            Console.WriteLine("".PadRight(50,'-'));
                            
                            Console.WriteLine(Environment.NewLine);
                            continue;
                        }
                    }
                    
                    Console.WriteLine("".PadRight(50,'-'));
                    Console.Write($"| Attacker: {attacker.Name}".PadRight(24) + "|");
                    Console.WriteLine($" Opponent: {opponent.Name}".PadRight(24) + "|");
                    Console.WriteLine("".PadRight(50,'-'));
                    
                    int damage = attacker.Attack(opponent);
                    
                    Console.WriteLine($"| {attacker.Name} attack is: {damage}".PadRight(49) + "|");
                    Console.WriteLine($"| {opponent.Name} health: {opponent.PrevHealth} -> {opponent.Health}".PadRight(49) + "|");
                    Console.WriteLine("".PadRight(50,'-'));
                    
                    Console.WriteLine(Environment.NewLine);
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
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
                new Sword(45, 2, "MorningStar"),
                new Shield(30, 2, "MorningShield")
            };

            List<Character> characters = new List<Character>()
            {
                new Hero("Geralt", 60, 15, 20, new Sword(50, 2, "Silver sword")),
                new Hero("Dovakhiin", 50, 5, 20, dovakhiinItems),
                new Dragon("Alduin", 70, 40, 60),
                new Dragon("Ivreirrinth", 70, 40, 60),
                new Wolf("Wolf1", 20, 18, 10),
                new Wolf("Wolf2", 20, 18, 10),
                new Wolf("Wolf3", 20, 18, 10),
                new Wolf("Wolf4", 20, 18, 10),
                new Wolf("Wolf5", 20, 18, 10),
            };


            characters.Sort();
            characters.Reverse();

            characters.ForEach(character => character.OpponentChange += (attacker, opponent) =>
            {
                if (opponent != null)
                    ConsoleWriter.NewLine($"{attacker.Name} selected new opponent {opponent?.Name}");
            });
            
            Dictionary<Character, double> strengths =
                characters.ToDictionary(character => character, character => character.GetStrength());


            ConsoleWriter.NewLine(ConsoleColor.Red, "CHARACTER", "STRENGTH");
            foreach ((Character character, double strength) in strengths)
                ConsoleWriter.NewLine(character.Name, $"{strength}");

            ConsoleWriter.NewFilledLine();
            double avg = strengths.Values.Average();

            ConsoleWriter.NewLine($"Average strength", $"{avg}");
            ConsoleWriter.NewFilledLine();

            ConsoleWriter.NewLine(ConsoleColor.Blue,"", "Over average", "");
            foreach ((Character character, double strength) in strengths.Where(character => character.Value > avg))
                ConsoleWriter.NewLine(character.Name, $"{strength}");
            ConsoleWriter.NewFilledLine();


            var min = strengths.Min(c => c.Value);

            ConsoleWriter.NewLine(ConsoleColor.Blue,"", "Weakest", "");
            foreach ((Character character, double strength) in strengths)
            {
                if (Math.Abs(strength - min) < 0.01)
                    ConsoleWriter.NewLine(character.Name, $"{min}");
            }

            ConsoleWriter.NewFilledLine();

            ConsoleWriter.NewLine(ConsoleColor.Blue,"", "Dragons", "");
            foreach ((Character character, double strength) in strengths.Where(character => character.Key is Dragon))
                ConsoleWriter.NewLine(character.Name, $"{strength}");
            ConsoleWriter.NewFilledLine();



            double maxDmg = characters.Average(character => character.GetMaxDamage()) / 2;
            double maxDef = characters.Average(character => character.GetMaxDefense()) / 4;
            
            ConsoleWriter.NewLine(ConsoleColor.Blue,"", $"dmg < {maxDmg}", "");
            foreach (var character in characters.FindAll(character => character.GetMaxDamage() < maxDmg)) 
                ConsoleWriter.NewLine(character.Name, $"{character.GetMaxDamage()} dmg");
            ConsoleWriter.NewFilledLine();

            
            ConsoleWriter.NewLine(ConsoleColor.Blue,"", $"def < {maxDef}", "");
            foreach (var character in characters.FindAll(character => character.GetMaxDefense() < maxDef)) 
                ConsoleWriter.NewLine(character.Name, $"{character.GetMaxDefense()} def");
            ConsoleWriter.NewFilledLine();
            

            for (int i = 1; Character.CanFight(characters); i++)
            {
                ConsoleWriter.NewBlankLine();
                ConsoleWriter.NewBlankLine();
                ConsoleWriter.NewFilledLine();
                ConsoleWriter.NewLine(ConsoleColor.Cyan, $"ROUND: {i}");
                ConsoleWriter.NewFilledLine();
                ConsoleWriter.NewBlankLine();

                int j = 0;

                ConsoleWriter.NewFilledLine();
                foreach (var attacker in characters.Where(attacker => attacker.IsAlive()))
                {
                    attacker.SelectOpponent(characters);
                    Character opponent = attacker.Opponent;

                    if (opponent == null)
                        break;
                    ++j;
                    
                    
                    ConsoleWriter.NewLine(ConsoleColor.Cyan, "Move", j.ToString());

                    switch (attacker.Escape())
                    {
                        case (EscapeEnum.Escaped):
                        {
                            ConsoleWriter.NewFilledLine();
                            ConsoleWriter.NewLine(ConsoleColor.Green, $"{attacker.Name} escaped...");
                            ConsoleWriter.NewFilledLine();
                            continue;
                        }
                        case (EscapeEnum.Tried):
                        {
                            ConsoleWriter.NewFilledLine();
                            ConsoleWriter.NewLine(ConsoleColor.DarkGray, $"{attacker.Name} tried to escape...");
                            ConsoleWriter.NewFilledLine();
                            continue;
                        }
                        case EscapeEnum.Cant:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    
                    ConsoleWriter.NewLine($"{attacker.Name}", "--|>>>>>>>", $"{opponent.Name}");

                    int damage = attacker.Attack(opponent);

                    ConsoleWriter.NewLine($"{attacker.Name} attack is: {damage}");
                    ConsoleWriter.NewLine(opponent.Health == 0 ? ConsoleColor.Red : default,
                        $"{opponent.Name} health: {opponent.PrevHealth} -> {opponent.Health}");
                    ConsoleWriter.NewFilledLine();
                }
            }


            ConsoleWriter.NewBlankLine();
            ConsoleWriter.NewLine("WINNERS!");
            ConsoleWriter.NewFilledLine();

            foreach (var character in characters)
            {
                if (character.IsAlive())
                    ConsoleWriter.NewLine(character.Name);
            }
            
            Console.ReadKey();
        }
    }
}
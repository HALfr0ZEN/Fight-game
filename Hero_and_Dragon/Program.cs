using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Hero_and_Dragon.Characters;
using Hero_and_Dragon.Enums;
using Hero_and_Dragon.Items;
using Hero_and_Dragon.Writers;

namespace Hero_and_Dragon
{
    class Program
    {
        static void Main(string[] args)
        {
            
            IWriter writer = ConsoleWriter.Instance;
            ConsoleWriter wr = writer as ConsoleWriter;
            
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
                    writer.NewLine($"{attacker.Name} selected new opponent {opponent.Name}");
            });

            Dictionary<Character, double> strengths =
                characters.ToDictionary(character => character, character => character.GetStrength());
            
            
            if (wr != null)
                wr.Color = ConsoleColor.Red;
            writer.NewLine( "CHARACTER", "STRENGTH"); //red

            foreach ((Character character, double strength) in strengths)
                writer.NewLine(character.Name, $"{strength}");

            writer.NewFilledLine();
            double avg = strengths.Values.Average();

            writer.NewLine($"Average strength", $"{avg}");
            writer.NewFilledLine();

            
            if (wr != null)
                wr.Color = ConsoleColor.Blue;
            writer.NewLine( "", "Over average", ""); //blue
            
            foreach ((Character character, double strength) in strengths.Where(character => character.Value > avg))
                writer.NewLine(character.Name, $"{strength}");
            writer.NewFilledLine();
            
            var min = strengths.Min(c => c.Value);

            if (wr != null)
                wr.Color = ConsoleColor.Blue;
            
            writer.NewLine( "", "Weakest", ""); 
            
            foreach ((Character character, double strength) in strengths)
            {
                if (Math.Abs(strength - min) < 0.01)
                    writer.NewLine(character.Name, $"{min}");
            }

            writer.NewFilledLine();

            if (wr != null)
                wr.Color = ConsoleColor.Blue;
            
            writer.NewLine( "", "Dragons", ""); //blue
            foreach ((Character character, double strength) in strengths.Where(character => character.Key is Dragon))
                writer.NewLine(character.Name, $"{strength}");
            writer.NewFilledLine();


            double maxDmg = characters.Average(character => character.GetMaxDamage()) / 2;
            double maxDef = characters.Average(character => character.GetMaxDefense()) / 4;

            if (wr != null)
                wr.Color = ConsoleColor.Blue;
            writer.NewLine("", $"dmg < {maxDmg}", "");
            foreach (var character in characters.FindAll(character => character.GetMaxDamage() < maxDmg))
                writer.NewLine(character.Name, $"{character.GetMaxDamage()} dmg");
            writer.NewFilledLine();


            if (wr != null)
                wr.Color = ConsoleColor.Blue;
            writer.NewLine( "", $"def < {maxDef}", "");
            foreach (var character in characters.FindAll(character => character.GetMaxDefense() < maxDef))
                writer.NewLine(character.Name, $"{character.GetMaxDefense()} def");
            writer.NewFilledLine();


            for (int i = 1; Character.CanFight(characters); i++)
            {
                writer.NewBlankLine();
                writer.NewBlankLine();
                writer.NewFilledLine();
                if (wr != null) wr.Color = ConsoleColor.Cyan;
                writer.NewLine( $"ROUND: {i}"); //cyan
                writer.NewFilledLine();
                writer.NewBlankLine();

                int j = 0;

                writer.NewFilledLine();
                foreach (var attacker in characters.Where(attacker => attacker.IsAlive()))
                {
                    attacker.SelectOpponent(characters);
                    Character opponent = attacker.Opponent;

                    if (opponent == null)
                        break;
                    ++j;
                    
                    if (wr != null)
                        wr.Color = ConsoleColor.Cyan;
                    writer.NewLine( "Move", j.ToString()); //cyan

                    switch (attacker.Escape())
                    {
                        case (EscapeEnum.Escaped):
                        {
                            writer.NewFilledLine();
                            if (wr != null) wr.Color = ConsoleColor.Green;
                            writer.NewLine( $"{attacker.Name} escaped..."); //green
                            writer.NewFilledLine();
                            continue;
                        }
                        case (EscapeEnum.Tried):
                        {
                            writer.NewFilledLine();
                            if (wr != null) wr.Color = ConsoleColor.DarkGray;
                            writer.NewLine($"{attacker.Name} tried to escape...");//DarkGray
                            writer.NewFilledLine();
                            continue;
                        }
                        case EscapeEnum.Cant:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    writer.NewLine($"{attacker.Name}", "--|>>>>>>>", $"{opponent.Name}");

                    int damage = attacker.Attack(opponent);

                    writer.NewLine($"{attacker.Name} attack is: {damage}");
                    if (opponent.Health == 0 && wr != null) wr.Color = ConsoleColor.Red;
                    writer.NewLine($"{opponent.Name} health: {opponent.PrevHealth} -> {opponent.Health}");
                    writer.NewFilledLine();
                }
            }


            writer.NewBlankLine();
            writer.NewLine("WINNERS!");
            writer.NewFilledLine();

            foreach (var character in characters)
            {
                if (character.IsAlive())
                    writer.NewLine(character.Name);
            }

            if(wr!=null) Console.ReadKey();
        }
    }
}
using System;

namespace Hero_and_Dragon
{
    class Dice {
        
        private readonly Random _random = new Random();
        public static Dice Instance { get; } = new Dice();
         
        static Dice() {}
        
        private Dice() {}
        

        public int Throw(int min, int max) => _random.Next(min, max);
        
        public int Throw(int max) => _random.Next(max);
        
        public double Throw() => _random.NextDouble();
        
    }
}
using System;

namespace Hero_and_Dragon
{
    /// <summary>
    /// Singleton class that provide just one instance on run
    /// </summary>
    class Dice {
        
        private readonly Random _random = new Random();
        public static Dice Instance { get; } = new Dice();
        
        private Dice() {}
        
        /// <summary>
        /// Throw method which will generate random number from given
        /// </summary>
        /// <param name="min">Min value than can be returned</param>
        /// <param name="max">Max value that can be returned</param>
        /// <returns>Random number between(or equal) min and max param</returns>
        public int Throw(int min, int max) => _random.Next(min, max);
        
        /// <summary>
        /// Throw method which will generate random number from 0 to given max
        /// </summary>
        /// <param name="max">Max value that can be returned</param>
        /// <returns>Random number between(or equal) 0 and max param</returns>
        public int Throw(int max) => _random.Next(max);
        
        /// <summary>
        /// Throw method which will generate random number from 0 to 1
        /// </summary>
        /// <returns>Number between 0 and 1</returns>
        public double Throw() => _random.NextDouble();
        
    }
}
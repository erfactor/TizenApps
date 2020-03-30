using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetris.GameLogic
{
    /// <summary>
    /// Class with extending helper methods
    /// </summary>
    public static class ExtensionMethods
    {
        private static Random _random = new Random();

        /// <summary>
        /// Returns random element of enumerable
        /// </summary>
        /// <param name="enumerable">Enumerable from which random element will be taken</param>
        /// <returns>Random element of enumerable</returns>
        public static T GetRandomElement<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ElementAt(_random.Next(enumerable.Count()));
        }

        /// <summary>
        /// Performs action on every element of enumerable
        /// </summary>
        /// <typeparam name="T">Source type</typeparam>
        /// <param name="source">Source enumerable</param>
        /// <param name="action">Action to perform on every element</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
            {
                action(element);
            }
        }

        /// <summary>
        /// Shuffles specified array
        /// </summary>
        /// <typeparam name="T">Array type</typeparam>
        /// <param name="array">Array to be shuffled</param>
        /// <returns>Shuffled array</returns>
        public static T[] Shuffle<T>(this T[] array)
        {
            for (int n = array.Length; n > 1;)
            {
                int k = _random.Next(n);
                --n;
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }

            return array;
        }
    }

}

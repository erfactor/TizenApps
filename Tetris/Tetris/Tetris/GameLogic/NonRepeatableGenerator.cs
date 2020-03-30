using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetris.GameLogic
{
    /// <summary>
    /// Generates elements such as they cant appear again for specified amount of times
    /// </summary>
    /// <typeparam name="T">Type of generated elements</typeparam>
    public class NonRepeatableGenerator<T>
    {
        private T[] _elements;
        private Queue<int> _usedIndices;

        /// <summary>
        /// Initalizes NonRepeatableGenerator class instance
        /// </summary>
        /// <param name="elements">Table of elements that will be returned by generator</param>
        /// <param name="times">How many times element will not appear again after it was generated</param>
        public NonRepeatableGenerator(T[] elements, int times)
        {
            _elements = elements;
            if (times >= elements.Length)
            {
                throw new Exception($"Not valid number, choose a value between 0 and elements.Length - 1");
            }

            var shuffled = Enumerable.Range(0, elements.Length).ToArray().Shuffle().Take(times);
            _usedIndices = new Queue<int>(shuffled);
        }

        /// <summary>
        /// Returns next element
        /// </summary>
        /// <returns>Element of specified type T</returns>
        public T GetNextElement()
        {
            List<int> availableIndices = Enumerable.Range(0, _elements.Length).Except(_usedIndices).ToList();
            int index = availableIndices.GetRandomElement();
            _usedIndices.Dequeue();
            _usedIndices.Enqueue(index);
            return _elements[index];
        }
    }
}
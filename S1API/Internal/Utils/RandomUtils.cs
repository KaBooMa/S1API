using System;
using System.Collections.Generic;

namespace S1API.Internal.Utils
{
    /// <summary>
    /// A utility class providing random selection functionality for lists and numeric ranges.
    /// </summary>
    public static class RandomUtils
    {
        /// <summary>
        /// Returns a random element from the provided list, or the default value of type T if the list is null or empty.
        /// </summary>
        /// <param name="list">The list from which to select a random element.</param>
        /// <returns>A randomly selected element from the list, or the default value of type T if the list is null or empty.</returns>
        public static T PickOne<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
            {
	            return default!;
            }

            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Returns a random element from a list that satisfies the given condition, with a maximum number of attempts.
        /// If no such element can be found within the allowed attempts, returns the default value of the type.
        /// </summary>
        /// <param name="list">The list of items to pick from.</param>
        /// <param name="isDuplicate">A function to determine if the selected item satisfies the duplicate condition.</param>
        /// <param name="maxTries">The maximum number of attempts to find a valid item.</param>
        /// <returns>A randomly selected item that satisfies the condition, or the default value of the type if no valid item is found.</returns>
        public static T PickUnique<T>(this IList<T> list, Func<T, bool> isDuplicate, int maxTries = 10)
        {
            if (list.Count == 0)
            {
	            return default!;
            }

            for (int i = 0; i < maxTries; i++)
            {
                T item = list.PickOne();
                if (!isDuplicate(item))
                {
	                return item;
                }
            }

            return default!;
        }

        /// <summary>
        /// Returns a specified number of unique random elements from a list.
        /// If the count exceeds the number of available elements, returns all elements in random order.
        /// </summary>
        /// <param name="list">The list of items to pick from.</param>
        /// <param name="count">The number of random items to pick.</param>
        /// <returns>A list containing the selected random items, or an empty list if the input list is null or empty.</returns>
        public static List<T> PickMany<T>(this IList<T> list, int count)
        {
            if (list.Count == 0)
            {
	            return new List<T>();
            }

            var copy = new List<T>(list);
            var result = new List<T>();

            for (int i = 0; i < count && copy.Count > 0; i++)
            {
                int index = UnityEngine.Random.Range(0, copy.Count);
                result.Add(copy[index]);
                copy.RemoveAt(index);
            }

            return result;
        }

        /// <summary>
        /// A private static instance of the System.Random class used for generating pseudo-random numbers
        /// in the RandomUtils utility class methods. It serves as the base random number generator for
        /// methods requiring randomness that do not rely on UnityEngine.Random.
        /// </summary>
        private static readonly System.Random SystemRng = new System.Random();

        /// <summary>
        /// Generates a random integer within the specified range.
        /// </summary>
        /// <param name="minInclusive">The inclusive lower bound of the random number.</param>
        /// <param name="maxExclusive">The exclusive upper bound of the random number.</param>
        /// <returns>A random integer greater than or equal to <paramref name="minInclusive"/> and less than <paramref name="maxExclusive"/>.</returns>
        public static int RangeInt(int minInclusive, int maxExclusive)
        {
            return SystemRng.Next(minInclusive, maxExclusive);
        }
    }
    
}
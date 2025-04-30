#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppInterop.Runtime.InteropTypes.Arrays;
#endif

using System;

namespace S1API.Internal.Utils
{
    /// <summary>
    /// Extensions for Arrays
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Add's an item to an existing array
        /// </summary>
        /// <param name="array"></param>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        public static T[] AddItemToArray<T>(this T[]? array, T item)
        {
            array ??= Array.Empty<T>();

            Array.Resize(ref array, array.Length + 1);
            array[^1] = item;

            return array;
        }

#if (IL2CPPMELON || IL2CPPBEPINEX)
        /// <summary>
        /// Add's an item to an existing <see cref="Il2CppReferenceArray{T}"/> instance.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="itemsToAdd"></param>
        /// <typeparam name="T"></typeparam>
        public static Il2CppReferenceArray<T> AddItemToArray<T>(this Il2CppReferenceArray<T>? array, params T[]? itemsToAdd) where T : Il2CppSystem.Object
        {
            var originalLength = array?.Length ?? 0;
            var additionalLength = itemsToAdd?.Length ?? 0;
            var newLength = originalLength + additionalLength;

            var result = new Il2CppReferenceArray<T>(newLength);
            for (var i = 0; i < originalLength; i++)
                result[i] = array[i];

            for (var i = 0; i < additionalLength; i++)
                result[originalLength + i] = itemsToAdd[i];

            return result;
        }
#endif
    }
}

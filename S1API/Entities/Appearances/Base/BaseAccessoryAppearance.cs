using System;
using System.Collections.Generic;
using System.Reflection;

namespace S1API.Entities.Appearances.Base
{
    /// <summary>
    /// The base accessory appearance class
    /// </summary>
    /// <remarks>This is used to track the properties within the AvatarSettings</remarks>
    public class BaseAccessoryAppearance
    {
        private static readonly Dictionary<Type, List<string>> _constCache = new Dictionary<Type, List<string>>();

        /// <summary>
        /// Retrieves and caches all public <c>const string</c> fields defined in the specified type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type from which to retrieve constant string fields. Must inherit from <see cref="BaseAccessoryAppearance"/>.
        /// </typeparam>
        /// <returns>
        /// A list of constant string values defined in the type <typeparamref name="T"/>.
        /// </returns>
        /// <remarks>
        /// Uses reflection to gather constants and caches them for future calls to improve performance.
        /// </remarks>
        internal static List<string> GetConstPaths<T>() where T : BaseAccessoryAppearance
        {
            var type = typeof(T);
            if (_constCache.TryGetValue(type, out var cached))
                return cached;

            var consts = new List<string>();
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            foreach (var field in fields)
            {
                if (field is { IsLiteral: true, IsInitOnly: false } && field.FieldType == typeof(string))
                    consts.Add((string)field.GetRawConstantValue());
            }

            _constCache[type] = consts;
            return consts;
        }
    }
}

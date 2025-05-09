using System;
using System.ComponentModel;
using System.Reflection;

namespace S1API.Internal.Utils
{
    /// <summary>
    /// A set of utility methods when working with enums.
    /// </summary>
    internal static class EnumUtils
    {
        /// <summary>
        /// Gets the value of the Description attribute for an enum value.
        /// </summary>
        /// <param name="value">The enum value</param>
        /// <returns>The description string or the enum value's name if no description is found</returns>
        internal static string GetDescriptionValue(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = fieldInfo?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? value.ToString();
        }
    }
}

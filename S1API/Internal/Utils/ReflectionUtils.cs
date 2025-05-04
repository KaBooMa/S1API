using S1API.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace S1API.Internal.Utils
{
    /// <summary>
    /// INTERNAL: Provides generic reflection based methods for easier API development
    /// </summary>
    internal static class ReflectionUtils
    {
        /// <summary>
        /// Identifies all classes derived from another class.
        /// </summary>
        /// <typeparam name="TBaseClass">The base class derived from.</typeparam>
        /// <returns>A list of all types derived from the base class.</returns>
        internal static List<Type> GetDerivedClasses<TBaseClass>()
        {
            List<Type> derivedClasses = new List<Type>();
            Assembly[] applicableAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => !assembly.FullName!.StartsWith("System") &&
                        !assembly.FullName!.StartsWith("Unity") &&
                        !assembly.FullName!.StartsWith("Il2Cpp") &&
                        !assembly.FullName!.StartsWith("mscorlib") &&
                        !assembly.FullName!.StartsWith("Mono.") &&
                        !assembly.FullName!.StartsWith("netstandard") &&
                        // Additional filters to resolve crashes in BepInEx
                        !assembly.FullName!.StartsWith("com.rlabrecque") &&
                        !assembly.FullName!.StartsWith("__Generated"))
                .ToArray();
            foreach (Assembly assembly in applicableAssemblies)
            {
	            derivedClasses.AddRange(assembly.GetTypes()
		            .Where(type => typeof(TBaseClass).IsAssignableFrom(type)
		                           && type != typeof(TBaseClass)
		                           && !type.IsAbstract));
            }

            return derivedClasses;
        }

        /// <summary>
        /// INTERNAL: Gets all types by their name.
        /// </summary>
        /// <param name="typeName">The name of the type.</param>
        /// <returns>The actual type identified by the name.</returns>
        internal static Type? GetTypeByName(string typeName)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                Type? foundType = assembly.GetTypes().FirstOrDefault(type => type.Name == typeName);
                if (foundType == null)
                {
	                continue;
                }

                return foundType;
            }

            return null;
        }

        /// <summary>
        /// INTERNAL: Recursively gets fields from a class down to the object type.
        /// </summary>
        /// <param name="type">The type you want to recursively search.</param>
        /// <param name="bindingFlags">The binding flags to apply during the search.</param>
        /// <returns></returns>
        internal static FieldInfo[] GetAllFields(Type? type, BindingFlags bindingFlags)
        {
            List<FieldInfo> fieldInfos = new List<FieldInfo>();
            while (type != null && type != typeof(object))
            {
                fieldInfos.AddRange(type.GetFields(bindingFlags));
                type = type.BaseType;
            }
            return fieldInfos.ToArray();
        }

        /// <summary>
        /// INTERNAL: Recursively searches for a method by name from a class down to the object type.
        /// </summary>
        /// <param name="type">The type you want to recursively search.</param>
        /// <param name="methodName">The name of the method you're searching for.</param>
        /// <param name="bindingFlags">The binding flags to apply during the search.</param>
        /// <returns></returns>
        public static MethodInfo? GetMethod(Type? type, string methodName, BindingFlags bindingFlags)
        {
            while (type != null && type != typeof(object))
            {
                MethodInfo? method = type.GetMethod(methodName, bindingFlags);
                if (method != null)
                {
	                return method;
                }

                type = type.BaseType;
            }

            return null;
        }
    }
}

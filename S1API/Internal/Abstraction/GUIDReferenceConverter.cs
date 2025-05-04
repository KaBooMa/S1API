using System;
using System.Reflection;
using Newtonsoft.Json;
using S1API.Internal.Utils;

namespace S1API.Internal.Abstraction
{
    /// <summary>
    /// INTERNAL: JSON Converter to handle GUID referencing classes when saved and loaded.
    /// </summary>
#pragma warning disable S101
    internal class GUIDReferenceConverter : JsonConverter
#pragma warning restore S101
    {
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType) =>
            typeof(IGUIDReference).IsAssignableFrom(objectType);

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is IGUIDReference reference)
            {
                writer.WriteValue(reference.GUID);
            }
            else
            {
                writer.WriteNull();
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            string? guid = reader.Value?.ToString();
            if (string.IsNullOrEmpty(guid))
            {
	            return null;
            }

            MethodInfo? getGUIDMethod = ReflectionUtils.GetMethod(objectType, "GetFromGUID", BindingFlags.NonPublic | BindingFlags.Static);
            if (getGUIDMethod == null)
            {
	            throw new Exception($"The type {objectType.Name} does not have a valid implementation of the GetFromGUID(string guid) method!");
            }

            return getGUIDMethod.Invoke(null, new object[] { guid });
        }

        /// <summary>
        /// TODO
        /// </summary>
        public override bool CanRead => true;
    }
}

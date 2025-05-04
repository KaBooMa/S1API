#if (MONOMELON || MONOBEPINEX)
using System.Collections.Generic;
#elif (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppSystem.Collections.Generic;
#endif

using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using S1API.Internal.Utils;
using S1API.Saveables;

namespace S1API.Internal.Abstraction
{
    /// <summary>
    /// INTERNAL: Generic wrapper for saveable classes.
    /// Intended for use within the API only.
    /// </summary>
    public abstract class Saveable : Registerable, ISaveable
    {
        /// <summary>
        /// TODO
        /// </summary>
        void ISaveable.LoadInternal(string folderPath) =>
            LoadInternal(folderPath);

        /// <summary>
        /// TODO
        /// </summary>
        internal virtual void LoadInternal(string folderPath)
        {
            FieldInfo[] saveableFields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (FieldInfo saveableField in saveableFields)
            {
                SaveableField? saveableFieldAttribute = saveableField.GetCustomAttribute<SaveableField>();
                if (saveableFieldAttribute == null)
                {
	                continue;
                }

                string filename = saveableFieldAttribute.SaveName.EndsWith(".json")
                    ? saveableFieldAttribute.SaveName
                    : $"{saveableFieldAttribute.SaveName}.json";

                string saveDataPath = Path.Combine(folderPath, filename);
                if (!File.Exists(saveDataPath))
                {
	                continue;
                }

                string json = File.ReadAllText(saveDataPath);
                Type type = saveableField.FieldType;
                object? value = JsonConvert.DeserializeObject(json, type, ISaveable.SerializerSettings);
                saveableField.SetValue(this, value);
            }

            OnLoaded();
        }

        /// <summary>
        /// TODO
        /// </summary>
        void ISaveable.SaveInternal(string path, ref List<string> extraSaveables) =>
            SaveInternal(path, ref extraSaveables);

        /// <summary>
        /// TODO
        /// </summary>
        internal virtual void SaveInternal(string folderPath, ref List<string> extraSaveables)
        {
            FieldInfo[] saveableFields = ReflectionUtils.GetAllFields(GetType(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (FieldInfo saveableField in saveableFields)
            {
                SaveableField? saveableFieldAttribute = saveableField.GetCustomAttribute<SaveableField>();
                if (saveableFieldAttribute == null)
                {
	                continue;
                }

                string saveFileName = saveableFieldAttribute.SaveName.EndsWith(".json")
                    ? saveableFieldAttribute.SaveName
                    : $"{saveableFieldAttribute.SaveName}.json";

                string saveDataPath = Path.Combine(folderPath, saveFileName);

                object? value = saveableField.GetValue(this);
                if (value == null)
                {
                    // Remove the save if the field is null
	                File.Delete(saveDataPath);
                }
                else
                {
                    // We add this to the extra saveables to prevent the game from deleting it
                    // Otherwise, it'll delete it after it finishes saving and does clean up
                    extraSaveables.Add(saveFileName);

                    // Write our data
                    string data = JsonConvert.SerializeObject(value, Formatting.Indented, ISaveable.SerializerSettings);
                    File.WriteAllText(saveDataPath, data);
                }
            }

            OnSaved();
        }

        /// <summary>
        /// TODO
        /// </summary>
        void ISaveable.OnLoaded() => OnLoaded();

        /// <summary>
        /// TODO
        /// </summary>
        protected virtual void OnLoaded() { }

        /// <summary>
        /// TODO
        /// </summary>
        void ISaveable.OnSaved() => OnSaved();

        /// <summary>
        /// TODO
        /// </summary>
        protected virtual void OnSaved() { }
    }
}

using System;

namespace S1API.Saveables
{
    /// <summary>
    /// Marks a field to be saved alongside the class instance.
    /// This attribute is intended to work across all custom game elements.
    /// (For example, custom NPCs, quests, etc.)
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class SaveableField : Attribute
    {
        /// <summary>
        /// What the save data should be named.
        /// </summary>
        internal string SaveName { get; }

        /// <summary>
        /// Base constructor for initializing a SaveableField.
        /// </summary>
        /// <param name="saveName"></param>
        public SaveableField(string saveName)
        {
            SaveName = saveName;
        }
    }
}
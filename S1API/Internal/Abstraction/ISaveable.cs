#if (MONOMELON || MONOBEPINEX)
using System.Collections.Generic;
#elif (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppSystem.Collections.Generic;
#endif

using Newtonsoft.Json;

namespace S1API.Internal.Abstraction
{
    /// <summary>
    /// INTERNAL: Provides rigidity for saveable instance wrappers.
    /// </summary>
    internal interface ISaveable : IRegisterable
    {
        /// <summary>
        /// INTERNAL: Called when saving the instance.
        /// </summary>
        /// <param name="path">Path to save to.</param>
        /// <param name="extraSaveables">Manipulation of the base game saveable lists.</param>
        void SaveInternal(string path, ref List<string> extraSaveables);
        
        /// <summary>
        /// INTERNAL: Called when loading the instance.
        /// </summary>
        /// <param name="folderPath"></param>
        void LoadInternal(string folderPath);
        
        /// <summary>
        /// Called when saving the instance.
        /// </summary>
        void OnSaved();
        
        /// <summary>
        /// Called when loading the instance.
        /// </summary>
        void OnLoaded();

        /// <summary>
        /// INTERNAL: Standard serialization settings to apply for all saveables.
        /// </summary>
        internal static JsonSerializerSettings SerializerSettings =>
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = new System.Collections.Generic.List<JsonConverter>() { new GUIDReferenceConverter() }
            };
    }
}

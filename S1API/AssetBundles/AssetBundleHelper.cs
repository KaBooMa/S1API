using System;
using System.IO;
using System.Reflection;
using UnityEngine;


namespace S1API.AssetBundles
{

    public static class AssetBundleHelper
    {
#if IL2CPP
        /// <summary>
        /// Loads an Il2Cpp AssetBundle from an embedded resource stream by name.
        /// </summary>
        /// <param name="resourceName">The full embedded resource name (including namespace path).</param>
        /// <returns>The loaded Il2CppAssetBundle, or throws on failure.</returns>
        public static  Il2CppAssetBundle GetAssetBundleFromStream(string resourceName)
        {
            // Attempt to find the embedded resource in the executing assembly
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new Exception($"Embedded resource '{resourceName}' not found in {assembly.FullName}."); // hoping these throws will be melon/bepinex-agnostic

                // Read the stream into a byte array
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);

                // Load the AssetBundle from memory
                var bundle = Il2CppAssetBundleManager.LoadFromMemory(data);
                if (bundle == null)
                    throw new Exception($"Failed to load AssetBundle from memory: {resourceName}");

                return bundle;
            }
        }
#else
        public static AssetBundle GetAssetBundleFromStream(string resourceName)
        {
            // Attempt to find the embedded resource in the executing assembly
            var assembly = Assembly.GetExecutingAssembly();

            var stream = assembly.GetManifestResourceStream(resourceName);

            return AssetBundle.LoadFromStream(stream);
        }
#endif


    }

}

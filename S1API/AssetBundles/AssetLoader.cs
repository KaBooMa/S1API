using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace S1API.AssetBundles
{

    public static class AssetLoader
    {
#if IL2CPP
        /// <summary>
        /// Loads an Il2Cpp AssetBundle from an embedded resource stream by name.
        /// </summary>
        /// <param name="fullResourceName">The full embedded resource name (including namespace path).</param>
        /// <returns>The loaded Il2CppAssetBundle, or throws on failure.</returns>
        public static  WrappedAssetBundle GetAssetBundleFromStream(string fullResourceName)
        {
            try
            {
                // Attempt to find the embedded resource in the executing assembly
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream(fullResourceName))
                {
                    if (stream == null)
                        throw new Exception($"Embedded resource '{fullResourceName}' not found in {assembly.FullName}."); // hoping these throws will be melon/bepinex-agnostic

                    // Read the stream into a byte array
                    byte[] data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);

                    // Load the AssetBundle from memory
                    Il2CppAssetBundle bundle = Il2CppAssetBundleManager.LoadFromMemory(data);
                    if (bundle == null)
                        throw new Exception($"Failed to load AssetBundle from memory: {fullResourceName}");

                    return new(bundle);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                Debug.LogError($"Error loading AssetBundle from stream: {ex.Message}");
                throw;
            }
        }
#else
        public static WrappedAssetBundle GetAssetBundleFromStream(string fullResourceName)
        {
            // Attempt to find the embedded resource in the executing assembly
            var assembly = Assembly.GetExecutingAssembly();

            var stream = assembly.GetManifestResourceStream(fullResourceName);

            return new(AssetBundle.LoadFromStream(stream));
        }
#endif

        /// <summary>
        /// 
        /// No need to type the assembly just the path stuff. 
        /// 
        /// Example if carassetbundle is in subfolder /bundles/: 
        /// 
        ///         GameObject myCar = Instantiate(EasyLoad<GameObject>("bundles.carassetbundle", "MyCarGameObject"));
        ///         
        /// </summary>
        public static T EasyLoad<T>(string bundle_name, string object_name) where T : UnityEngine.Object
        {
            return EasyLoad<T>(bundle_name, object_name, Assembly.GetExecutingAssembly(), out _);
        }

        public static T EasyLoad<T>(string bundle_name, string object_name, out WrappedAssetBundle bundle) where T : UnityEngine.Object
        {
            return EasyLoad<T>(bundle_name, object_name, Assembly.GetExecutingAssembly(), out bundle);
        }

        public static T EasyLoad<T>(string bundle_name, string object_name, Assembly assemblyOverride) where T : UnityEngine.Object
        {
            return EasyLoad<T>(bundle_name, object_name, assemblyOverride, out _);
        }

        public static T EasyLoad<T>(string bundle_name, string object_name, Assembly assemblyOverride, out WrappedAssetBundle bundle) where T : UnityEngine.Object
        {
            // Get the asset bundle from the assembly
            bundle = GetAssetBundleFromStream($"{assemblyOverride.GetName().Name}.{bundle_name}");

            // Load the asset from the bundle
            return bundle.LoadAsset<T>(object_name);
        }

    }

}

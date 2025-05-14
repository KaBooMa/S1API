#if IL2CPPMELON || IL2CPPBEPINEX
using Il2CppSystem;
using AssetBundle = UnityEngine.Il2CppAssetBundle;
#else
using UnityEngine;
using System;
#endif

using Object = UnityEngine.Object;

namespace S1API.AssetBundles
{
    /// <summary>
    /// INTERNAL: Wrapper around <see cref="AssetBundle"/> instance.
    /// </summary>
    public class WrappedAssetBundle
    {
        /// <summary>
        /// Gets a value indicating whether this asset bundle is a streamed scene asset bundle.
        /// </summary>
        public bool IsStreamedAssetBundle => _realBundle.isStreamedSceneAssetBundle;

        /// <summary>
        /// The actual Unity AssetBundle being wrapped.
        /// </summary>
        private readonly AssetBundle _realBundle;

        /// <summary>
        /// Initializes a new instance of the <see cref="WrappedAssetBundle"/> class.
        /// </summary>
        /// <param name="realBundle">The actual Unity AssetBundle to wrap.</param>
        public WrappedAssetBundle(AssetBundle realBundle)
        {
            _realBundle = realBundle;
        }

        /// <summary>
        /// Checks if the asset bundle contains a specific asset by name.
        /// </summary>
        public bool Contains(string name) => _realBundle.Contains(name);

        /// <summary>
        /// Returns all asset names contained in the asset bundle.
        /// </summary>
        public string[] GetAllAssetNames() => _realBundle.GetAllAssetNames();

        /// <summary>
        /// Returns all scene paths contained in the asset bundle.
        /// </summary>
        public string[] GetAllScenePaths() => _realBundle.GetAllScenePaths();

        /// <summary>
        /// Loads an asset by name as a generic <see cref="Object"/>.
        /// </summary>
        public Object Load(string name) => LoadAsset(name);

        /// <summary>
        /// Loads an asset by name as a generic <see cref="Object"/>.
        /// </summary>
        public Object LoadAsset(string name) => LoadAsset<Object>(name);

        /// <summary>
        /// Loads an asset by name and casts it to the specified type.
        /// </summary>
        public T Load<T>(string name) where T : Object => LoadAsset<T>(name);

        /// <summary>
        /// Loads an asset by name and casts it to the specified type.
        /// </summary>
        public T LoadAsset<T>(string name) where T : Object => _realBundle.LoadAsset<T>(name);

        /// <summary>
        /// Loads an asset by name using a <see cref="Type"/> object.
        /// </summary>
        public Object Load(string name, Type type) => LoadAsset(name, type);

        /// <summary>
        /// Loads an asset by name using a <see cref="Type"/> object.
        /// </summary>
        public Object LoadAsset(string name, Type type) => _realBundle.LoadAsset(name, type);

        /// <summary>
        /// Asynchronously loads an asset by name as a generic <see cref="Object"/>.
        /// </summary>
        public WrappedAssetBundleRequest LoadAssetAsync(string name) => LoadAssetAsync<Object>(name);

        /// <summary>
        /// Asynchronously loads an asset by name and casts it to the specified type.
        /// </summary>
        public WrappedAssetBundleRequest LoadAssetAsync<T>(string name) where T : Object =>
            new WrappedAssetBundleRequest(_realBundle.LoadAssetAsync<T>(name));

        /// <summary>
        /// Asynchronously loads an asset by name using a <see cref="Type"/> object.
        /// </summary>
        public WrappedAssetBundleRequest LoadAssetAsync(string name, Type type) =>
            new WrappedAssetBundleRequest(_realBundle.LoadAssetAsync(name, type));

        /// <summary>
        /// Loads all assets from the bundle as <see cref="Object"/> instances.
        /// </summary>
        public Object[] LoadAll() => LoadAllAssets();

        /// <summary>
        /// Loads all assets from the bundle as <see cref="Object"/> instances.
        /// </summary>
        public Object[] LoadAllAssets() => LoadAllAssets<Object>();

        /// <summary>
        /// Loads all assets from the bundle and casts them to the specified type.
        /// </summary>
        public T[] LoadAllAssets<T>() where T : Object => _realBundle.LoadAllAssets<T>();

        /// <summary>
        /// Loads all assets from the bundle using a <see cref="Type"/> object.
        /// </summary>
        public Object[] LoadAllAssets(Type type) => _realBundle.LoadAllAssets(type);

        /// <summary>
        /// Loads an asset and all of its sub-assets by name as <see cref="Object"/> instances.
        /// </summary>
        public Object[] LoadAssetWithSubAssets(string name) => LoadAssetWithSubAssets<Object>(name);

        /// <summary>
        /// Loads an asset and its sub-assets by name and casts them to the specified type.
        /// </summary>
        public T[] LoadAssetWithSubAssets<T>(string name) where T : Object => _realBundle.LoadAssetWithSubAssets<T>(name);

        /// <summary>
        /// Loads an asset and its sub-assets by name using a <see cref="Type"/> object.
        /// </summary>
        public Object[] LoadAssetWithSubAssets(string name, Type type) => _realBundle.LoadAssetWithSubAssets(name, type);

        /// <summary>
        /// Unloads the asset bundle and optionally unloads all loaded objects.
        /// </summary>
        /// <param name="unloadAllLoadedObjects">Whether to unload all loaded objects as well.</param>
        public void Unload(bool unloadAllLoadedObjects) => _realBundle.Unload(unloadAllLoadedObjects);
    }
}

/* Might need this if the above fails


public static class Il2CppStringArrayExtensions
{
    public static string[] ToManagedArray(this Il2CppStringArray il2cppStrings)
    {
        string[] managedStrings = new string[il2cppStrings.Length];
        for (int i = 0; i < il2cppStrings.Length; i++)
        {
            managedStrings[i] = il2cppStrings[i];
        }
        return managedStrings;
    }
}

public static class Il2CppObjectArrayExtensions
{
    public static T[] ToManagedArray<T>(this Il2CppReferenceArray<T> il2cppObjects) where T : Object
    {
        T[] managedObjects = new T[il2cppObjects.Length];
        for (int i = 0; i < il2cppObjects.Length; i++)
        {
            managedObjects[i] = il2cppObjects[i];
        }
        return managedObjects;
    }
}
*/


/*
 *
 * [12:57:44.463] [ManorMod] Unhandled exception in coroutine. It will not continue executing.
System.MissingMethodException: Method not found: 'UnityEngine.AssetBundle UnityEngine.AssetBundle.LoadFromStream(System.IO.Stream)'.
   at S1API.AssetBundles.AssetLoader.GetAssetBundleFromStream(String fullResourceName)
   at S1API.AssetBundles.AssetLoader.EasyLoad[T](String bundle_name, String object_name, Assembly assemblyOverride, WrappedAssetBundle& bundle)
   at S1API.AssetBundles.AssetLoader.EasyLoad[T](String bundle_name, String object_name)
   at ManorMod.Core.LoadAssetBundle()+MoveNext()
   at MelonLoader.Support.MonoEnumeratorWrapper.MoveNext() in D:\a\MelonLoader\MelonLoader\Dependencies\SupportModules\Il2Cpp\MonoEnumeratorWrapper.cs:line 39
[12:57:48.792] [ManorMod] System.MissingMethodException: Method not found: 'Void UnityEngine.Events.UnityAction..ctor(System.Object, IntPtr)'.
   at ManorMod.Core.OnLateInitializeMelon()
   at MelonLoader.MelonEvent.<>c.<Invoke>b__1_0(LemonAction x) in D:\a\MelonLoader\MelonLoader\MelonLoader\Melons\Events\MelonEvent.cs:line 174
   at MelonLoader.MelonEventBase`1.Invoke(Action`1 delegateInvoker) in D:\a\MelonLoader\MelonLoader\MelonLoader\Melons\Events\MelonEvent.cs:line 143


*/

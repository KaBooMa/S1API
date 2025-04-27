using UnityEngine;

#if IL2CPP
using Type = Il2CppSystem.Type;
using AssetBundle = UnityEngine.Il2CppAssetBundle;
using AssetBundleRequest = UnityEngine.Il2CppAssetBundleRequest;
#else
using Type = System.Type;
#endif

using Object = UnityEngine.Object;


namespace S1API.AssetBundles
{
    
    /// <summary>
    ///     Works just like the AssetBundle type, it's just a proxy, but will use Il2CppAssetBundle if it needs to so you don't have to worry about it.
    /// </summary>
    public class WrappedAssetBundle
    {
        public bool isStreamedAssetBundle => _realBundle.isStreamedSceneAssetBundle;

        public AssetBundle _realBundle;

        public WrappedAssetBundle(AssetBundle realBundle)
        {
            _realBundle = realBundle;
        }


        public bool Contains(string name)
        {
            return _realBundle.Contains(name);
        }

        public string[] AllAssetNames()
        {
            return GetAllAssetNames();
        }

        public string[] GetAllAssetNames()
        {
            return _realBundle.GetAllAssetNames();
        }

        public string[] AllScenePaths()
        {
            return GetAllScenePaths();
        }

        public string[] GetAllScenePaths()
        {
            return _realBundle.GetAllScenePaths();
        }

        public Object Load(string name)
        {
            return LoadAsset(name);
        }

        public Object LoadAsset(string name)
        {
            return this.LoadAsset<Object>(name);
        }

        public T Load<T>(string name) where T : Object
        {
            return LoadAsset<T>(name);
        }

        public T LoadAsset<T>(string name) where T : Object
        {
            return _realBundle.LoadAsset<T>(name);
        }

        public Object Load(string name, Type type)
        {
            return LoadAsset(name, type);
        }

        public Object LoadAsset(string name, Type type)
        {
            return _realBundle.LoadAsset(name, type);
        }

        public WrappedAssetBundleRequest LoadAssetAsync(string name)
        {
            return this.LoadAssetAsync<Object>(name);
        }

        public WrappedAssetBundleRequest LoadAssetAsync<T>(string name) where T : Object
        {
            return new(_realBundle.LoadAssetAsync<T>(name));
        }

        public WrappedAssetBundleRequest LoadAssetAsync(string name, Type type)
        {
            return new(_realBundle.LoadAssetAsync(name, type));
        }

        public Object[] LoadAll()
        {
            return LoadAllAssets();
        }

        public Object[] LoadAllAssets()
        {
            return this.LoadAllAssets<Object>();
        }

        public T[] LoadAllAssets<T>() where T : Object
        {
            return _realBundle.LoadAllAssets<T>();
        }

        public Object[] LoadAllAssets(Type type)
        {
            return _realBundle.LoadAllAssets(type);
        }

        public Object[] LoadAssetWithSubAssets(string name)
        {
            return this.LoadAssetWithSubAssets<Object>(name);
        }

        public T[] LoadAssetWithSubAssets<T>(string name) where T : Object
        {
            return _realBundle.LoadAssetWithSubAssets<T>(name);
        }

        public Object[] LoadAssetWithSubAssets(string name, Type type)
        {
            return _realBundle.LoadAssetWithSubAssets(name, type);
        }

        public void Unload(bool unloadAllLoadedObjects)
        {
            _realBundle.Unload(unloadAllLoadedObjects);
        }
    }


    /// <summary>
    ///     Works just like the AssetBundleRequest type, it's just a proxy, but will use Il2CppAssetBundleRequest if it needs to so you don't have to worry about it.
    /// </summary>
    public class WrappedAssetBundleRequest
    {
        public AssetBundleRequest _realRequest;

        public WrappedAssetBundleRequest(AssetBundleRequest realRequest)
        {
            _realRequest = realRequest;
        }

        public Object asset => _realRequest.asset;

        public Object[] allAssets => _realRequest.allAssets;
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



using UnityEngine;

#if IL2CPPBEPINEX || IL2CPPMELON
using AssetBundleRequest = UnityEngine.Il2CppAssetBundleRequest;
#endif

namespace S1API.AssetBundles
{
    /// <summary>
    /// INTERNAL: Wrapper around <see cref="AssetBundleRequest"/> instance.
    /// </summary>
    public class WrappedAssetBundleRequest
    {
        private readonly AssetBundleRequest _realRequest;

        /// <summary>
        /// INTERNAL: Default constructor for <see cref="WrappedAssetBundleRequest"/>
        /// </summary>
        /// <param name="realRequest"></param>
        internal WrappedAssetBundleRequest(AssetBundleRequest realRequest)
        {
            _realRequest = realRequest;
        }

        /// <summary>
        /// The requested <see cref="Object"/> asset instance.
        /// </summary>
        public Object Asset => _realRequest.asset;

        /// <summary>
        /// All Assets in the <see cref="AssetBundleRequest"/>.
        /// </summary>
        public Object[] AllAssets => _realRequest.allAssets;
    }
}

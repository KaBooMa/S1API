#if (MONOMELON || IL2CPPMELON)
using MelonLoader;

[assembly: MelonInfo(typeof(S1API.S1API), "S1API", "{VERSION_NUMBER}", "KaBooMa")]

namespace S1API
{
    /// <summary>
    /// Not currently utilized by S1API.
    /// </summary>
    public class S1API : MelonMod
    {
    }
}
#elif (IL2CPPBEPINEX || MONOBEPINEX)
using BepInEx;

#if MONOBEPINEX
using BepInEx.Unity.Mono;
#elif IL2CPPBEPINEX
using BepInEx.Unity.IL2CPP;
#endif

using HarmonyLib;

namespace S1API
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class S1API : 
#if MONOBEPINEX
        BaseUnityPlugin
#elif IL2CPPBEPINEX
        BasePlugin
#endif
    {
#if MONOBEPINEX
        private void Awake()
#elif IL2CPPBEPINEX
        public override void Load()
#endif
        {
            new Harmony("com.S1API").PatchAll();
        }
    }
}
#endif

#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1PlayerScripts = Il2CppScheduleOne.PlayerScripts;
#else
using S1PlayerScripts = ScheduleOne.PlayerScripts;
#endif


using System.Linq;
using HarmonyLib;
using S1API.Entities;

namespace S1API.Internal.Patches
{
    /// <summary>
    /// INTERNAL: Patches to apply to the Players for tracking.
    /// </summary>
    [HarmonyPatch]
#pragma warning disable S1118
    internal class PlayerPatches
#pragma warning restore S1118
    {
        /// <summary>
        /// INTERNAL: Adds players to the player list upon wake.
        /// </summary>
        /// <param name="__instance">The player to add.</param>
        [HarmonyPatch(typeof(S1PlayerScripts.Player), "Awake")]
        [HarmonyPostfix]
        private static void PlayerAwake(S1PlayerScripts.Player __instance) =>
            new Player(__instance);


        /// <summary>
        /// INTERNAL: Removes players from the player list upon destruction.
        /// </summary>
        /// <param name="__instance">The player to remove.</param>
        [HarmonyPatch(typeof(S1PlayerScripts.Player), "OnDestroy")]
        [HarmonyPostfix]
        private static void PlayerOnDestroy(S1PlayerScripts.Player __instance) =>
            Player.All.Remove(Player.All.First(player => player.S1Player == __instance));
    }
}

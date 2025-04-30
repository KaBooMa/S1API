#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1GameTime = Il2CppScheduleOne.GameTime;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1GameTime = ScheduleOne.GameTime;
#endif

using System;
using HarmonyLib;
using S1API.GameTime;

namespace S1API.Internal.Patches
{
    /// <summary>
    /// INTERNAL: All patches related to NPCs.
    /// </summary>
    [HarmonyPatch]
    internal class TimePatches
    {
        /// <summary>
        /// Patch performed for when the time manager wakes up.
        /// </summary>
        /// <param name="__instance">Instance of the time manager</param>
        [HarmonyPatch(typeof(S1GameTime.TimeManager), "Awake")]
        [HarmonyPostfix]
        private static void TimeManagerAwake(S1GameTime.TimeManager __instance)
        {
            // Attach our OnDayPass Action to the time manager's Action
            void DayPass()
            {
                TimeManager.OnDayPass.Invoke();
            }

            __instance.onDayPass += (Action)DayPass;
        }
    }
}

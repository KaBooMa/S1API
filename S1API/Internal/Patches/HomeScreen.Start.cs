using System;
using HarmonyLib;
using UnityEngine.SceneManagement;
using S1API.Internal.Utils;
using S1API.Internal.Abstraction;
using S1API.PhoneApp;
using S1API.Logging;

#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.UI.Phone;
#else
using ScheduleOne.UI.Phone;
#endif

namespace S1API.Internal.Patches
{
    /// <summary>
    /// A Harmony patch for the Start method of the HomeScreen class, facilitating the registration and initialization of PhoneApps.
    /// </summary>
    [HarmonyPatch(typeof(HomeScreen), "Start")]
    internal static class HomeScreenStartPatch
    {
        /// <summary>
        /// A logging instance used for handling log messages pertaining to PhoneApp registration
        /// and operations. Provides methods to log messages with different severity levels such
        /// as Info, Warning, Error, and Fatal.
        /// </summary>
        private static readonly Log _logger = new Log("PhoneApp");

        /// <summary>
        /// Executes after the HomeScreen's Start method to handle the registration
        /// and initialization of PhoneApps.
        /// </summary>
        /// <param name="__instance">The HomeScreen instance being targeted in the patch.</param>
#pragma warning disable S1144
        private static void Postfix(HomeScreen __instance)
#pragma warning restore S1144
        {
            if (__instance == null)
            {
	            return;
            }

            // Re-register all PhoneApps
            var phoneApps = ReflectionUtils.GetDerivedClasses<PhoneApp.PhoneApp>();
            foreach (var type in phoneApps)
            {
                _logger.Msg($"Found phone app: {type.FullName}");

                if (type.GetConstructor(Type.EmptyTypes) == null)
                {
	                continue;
                }

                try
                {
                    var phoneAppInstance = (PhoneApp.PhoneApp)Activator.CreateInstance(type)!;
                    ((IRegisterable)phoneAppInstance).CreateInternal();
                    phoneAppInstance.SpawnUI(__instance);
                    phoneAppInstance.SpawnIcon(__instance);
                }
                catch (Exception e)
                {
                    _logger.Warning($"[PhoneApp] Failed to register {type.FullName}: {e.Message}");
                }
            }
        }
    }
}

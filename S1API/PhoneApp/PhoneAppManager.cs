using System.Collections;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;

namespace S1API.PhoneApp
{
    /// <summary>
    /// Central manager for spawning phone apps.
    /// Usage: PhoneAppManager.Register(new MyCustomApp());
    /// </summary>
    public static class PhoneAppManager
    {
        private static readonly List<PhoneApp> registeredApps = new List<PhoneApp>();
        private static bool initialized = false;

        /// <summary>
        /// Register your custom app. Should be called from OnApplicationStart().
        /// </summary>
        public static void Register(PhoneApp app)
        {
            registeredApps.Add(app);
        }

        /// <summary>
        /// Call this once after the game scene is loaded.
        /// Automatically initializes all registered apps.
        /// </summary>
        public static void InitAll(MelonLogger.Instance logger)
        {
            if (initialized) return;
            initialized = true;
            MelonCoroutines.Start(DelayedInitAll(logger));
        }

        private static IEnumerator DelayedInitAll(MelonLogger.Instance logger)
        {
            yield return new WaitForSeconds(5f);

            foreach (var app in registeredApps)
            {
                app.Init(logger);
            }
        }
    }
}

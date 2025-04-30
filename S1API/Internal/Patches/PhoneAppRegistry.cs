using System.Collections.Generic;

namespace S1API.Internal.Patches
{
    /// <summary>
    /// Provides functionality for managing the registration of custom phone applications.
    /// </summary>
    /// <remarks>
    /// This static class serves as a registry for tracking instances of phone applications.
    /// Applications are added to a centralized list which can then be used for initialization or
    /// interacting with registered applications at runtime.
    /// </remarks>
    internal static class PhoneAppRegistry
    {
        /// <summary>
        /// A static readonly list that stores instances of phone applications registered via the <c>PhoneAppRegistry</c>.
        /// </summary>
        /// <remarks>
        /// This list holds all registered instances of <see cref="PhoneApp"/> objects. Applications are added to this collection
        /// whenever they are registered using the <c>PhoneAppRegistry.Register</c> method, which is typically called automatically
        /// during the application's lifecycle.
        /// It serves as a central repository for all in-game phone applications, enabling other systems to access and manage
        /// these registered apps efficiently.
        /// </remarks>
        public static readonly List<PhoneApp.PhoneApp> RegisteredApps = new List<PhoneApp.PhoneApp>();

        /// <summary>
        /// Registers a specified phone app into the phone application registry.
        /// </summary>
        /// <param name="app">The PhoneApp instance to be registered.</param>
        public static void Register(PhoneApp.PhoneApp app)
        {
            RegisteredApps.Add(app);
        }
    }
}
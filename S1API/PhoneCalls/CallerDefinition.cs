#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1CallerID = Il2CppScheduleOne.ScriptableObjects.CallerID;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1CallerID = ScheduleOne.ScriptableObjects.CallerID;
#endif

using UnityEngine;

namespace S1API.PhoneCalls
{
    /// <summary>
    /// Represents the definition of a <see cref="S1CallerID"/> instance.
    /// </summary>
    public class CallerDefinition
    {
        /// <summary>
        /// INTERNAL: The stored reference to the caller entry in-game
        /// </summary>
        internal readonly S1CallerID S1CallerIDEntry;

        /// <summary>
        /// INTERNAL: Creates a caller id entry from an in-game caller id instance.
        /// </summary>
        /// <param name="s1CallerID"></param>
        internal CallerDefinition(S1CallerID s1CallerID)
            => S1CallerIDEntry = s1CallerID;

        /// <summary>
        /// The name of the caller.
        /// </summary>
        public string Name
        {
            get => S1CallerIDEntry.Name;
            set => S1CallerIDEntry.Name = value;
        }

        /// <summary>
        /// The profile picture of the caller.
        /// </summary>
        public Sprite? ProfilePicture
        {
            get => S1CallerIDEntry.ProfilePicture;
            set => S1CallerIDEntry.ProfilePicture = value;
        }
    }
}

#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1ScriptableObjects = Il2CppScheduleOne.ScriptableObjects;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1ScriptableObjects = ScheduleOne.ScriptableObjects;
#endif

#if (MONOMELON || MONOBEPINEX)
using System.Collections.Generic;
#elif (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppSystem.Collections.Generic;
#endif

using System;
using S1API.Entities;
using S1API.Internal.Utils;
using UnityEngine;

namespace S1API.PhoneCalls
{
    /// <summary>
    /// Represents the definition of a phone call, including the caller, stages, and associated data.
    /// This abstract class provides the base structure for creating custom phone calls.
    /// </summary>
    public abstract class PhoneCallDefinition
    {
        // ReSharper disable once MemberCanBePrivate.Global
        /// <summary>
        /// The caller of the <see cref="PhoneCallDefinition"/> instance
        /// </summary>
#pragma warning disable S1104
        public CallerDefinition? Caller;
#pragma warning restore S1104

        // ReSharper disable once MemberCanBePrivate.Global
        /// <summary>
        /// The original <see cref="S1ScriptableObjects.PhoneCallData"/> instance
        /// </summary>
        public readonly S1ScriptableObjects.PhoneCallData S1PhoneCallData;

        // ReSharper disable once MemberCanBePrivate.Global
        /// <summary>
        /// A list of all stage entries added to this phone call.
        /// </summary>
        protected readonly List<CallStageEntry> StageEntries = new List<CallStageEntry>();

        /// <summary>
        /// INTERNAL: Public constructor used for instancing a new <see cref="S1ScriptableObjects.PhoneCallData"/>
        /// </summary>
        /// <param name="name">The name of the caller</param>
        /// <param name="profilePicture">The sprite of the caller</param>
        protected PhoneCallDefinition(string name, Sprite? profilePicture = null)
        {
            S1PhoneCallData = ScriptableObject.CreateInstance<S1ScriptableObjects.PhoneCallData>();
            S1PhoneCallData.Stages ??= Array.Empty<S1ScriptableObjects.PhoneCallData.Stage>();

            _ = AddCallerID(name, profilePicture);
        }

        /// <summary>
        /// INTERNAL: Public constructor used for instancing a new <see cref="S1ScriptableObjects.PhoneCallData"/>
        /// </summary>
        /// <param name="npcCallerID">The <see cref="NPC"/> instance to use for the caller</param>
        protected PhoneCallDefinition(NPC? npcCallerID)
        {
            S1PhoneCallData = ScriptableObject.CreateInstance<S1ScriptableObjects.PhoneCallData>();
            S1PhoneCallData.Stages ??= Array.Empty<S1ScriptableObjects.PhoneCallData.Stage>();

            _ = AddCallerID(npcCallerID);
        }

        /// <summary>
        /// Set's a new CallerID definition to the PhoneCall
        /// </summary>
        /// <param name="name">The name of the caller</param>
        /// <param name="profilePicture">The sprite of the caller</param>
        /// <returns>A reference to the CallerID definition</returns>
        protected CallerDefinition AddCallerID(string name, Sprite? profilePicture = null)
        {
            S1ScriptableObjects.CallerID originalCallerID = ScriptableObject.CreateInstance<S1ScriptableObjects.CallerID>();
            originalCallerID.Name = name;
            originalCallerID.ProfilePicture = profilePicture;
            S1PhoneCallData.CallerID = originalCallerID;

            Caller = new CallerDefinition(originalCallerID)
            {
                Name = name,
                ProfilePicture = profilePicture
            };
            return Caller;
        }

        /// <summary>
        /// Set's a new CallerID definition based of an existing <see cref="NPC"/> instance.
        /// </summary>
        /// <param name="npc">The <see cref="NPC"/> instance to use for the caller</param>
        /// <returns>A reference to the CallerID definition</returns>
        protected CallerDefinition AddCallerID(NPC? npc)
        {
            S1ScriptableObjects.CallerID originalCallerID = ScriptableObject.CreateInstance<S1ScriptableObjects.CallerID>();
            originalCallerID.Name = npc?.FullName ?? "Unknown Caller";
            originalCallerID.ProfilePicture = npc?.Icon;
            S1PhoneCallData.CallerID = originalCallerID;

            Caller = new CallerDefinition(originalCallerID)
            {
                Name = npc?.FullName ?? "Unknown Caller",
                ProfilePicture = npc?.Icon
            };
            return Caller;
        }

        /// <summary>
        /// Add's a <see cref="CallStageEntry"/> instance to the <see cref="S1PhoneCallData"/>
        /// </summary>
        /// <param name="text">The text to display in this stage</param>
        /// <returns>A reference to the Stage entry</returns>
        protected CallStageEntry AddStage(string text)
        {
            S1ScriptableObjects.PhoneCallData.Stage originalStage = new S1ScriptableObjects.PhoneCallData.Stage
            {
                Text = text
            };
            S1PhoneCallData.Stages = S1PhoneCallData.Stages.AddItemToArray(originalStage);

            CallStageEntry callStageEntry = new CallStageEntry(originalStage)
            {
                Text = text
            };
            StageEntries.Add(callStageEntry);

            return callStageEntry;
        }

        /// <summary>
        /// Completes the <see cref="S1ScriptableObjects.PhoneCallData"/> instance.
        /// </summary>
        public void Completed() => S1PhoneCallData.Completed();
    }
}

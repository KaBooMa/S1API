using System.Collections.Generic;
using UnityEngine;
#if (IL2CPPMELON || MONOMELON)
using MelonLoader;
#endif

#if IL2CPPMELON
using Il2CppScheduleOne.Dialogue;
using Il2CppScheduleOne.NPCs;
using Il2CppScheduleOne.NPCs.Schedules;
using Il2CppFishNet;
#else
using ScheduleOne.Dialogue;
using ScheduleOne.NPCs;
using ScheduleOne.NPCs.Schedules;
using FishNet;
#endif

namespace S1API.Dialogues
{
    /// <summary>
    /// The DialogueInjector class is a static utility that facilitates the injection of custom dialogue entries
    /// into a game's dialogue system at runtime. It provides methods for registering custom dialogue injections
    /// and ensures that these injections are processed correctly within the update loop.
    /// </summary>
    public static class DialogueInjector
    {
        /// <summary>
        /// Represents a collection of dialogue injections waiting to be processed for corresponding NPCs in the game.
        /// This variable stores pending dialogue link or choice modifications that need to be applied to NPC dialogue systems
        /// and is used during a coroutine process to find the relevant NPC and complete the injection.
        /// </summary>
        private static System.Collections.Generic.List<DialogueInjection> _pendingInjections = new System.Collections.Generic.List<DialogueInjection>();

        /// <summary>
        /// A boolean variable that indicates whether the update loop for injecting dialogue is currently hooked.
        /// When true, the update loop has been hooked and is actively monitoring for pending dialogue injections; otherwise, it has not been hooked.
        /// </summary>
        private static bool _isHooked = false;

        /// <summary>
        /// Registers a dialogue injection to be processed in the update loop.
        /// </summary>
        /// <param name="injection">An instance of <see cref="DialogueInjection"/> representing the dialogue to be injected into the game.</param>
        public static void Register(DialogueInjection injection)
        {
            _pendingInjections.Add(injection);
            HookUpdateLoop();
        }

        /// <summary>
        /// Ensures that the dialogue injection system's loop is hooked into the game's update cycle.
        /// This method starts a coroutine to monitor and inject pending dialogue changes into NPCs.
        /// </summary>
        /// <remarks>
        /// This method prevents multiple hookups by checking if the injection system is already active using an internal flag.
        /// If not already hooked, the method initializes a coroutine that processes and injects queued dialogue data into the corresponding NPCs.
        /// </remarks>
        private static void HookUpdateLoop()
        {
            if (_isHooked)
                return;
            _isHooked = true;

            // @TODO: Check whether coroutines can be started with this.
#if (IL2CPPMELON || MONOMELON)
            MelonCoroutines.Start(WaitForNPCsAndInject());
#elif (IL2CPPBEPINEX || MONOBEPINEX)
            InstanceFinder.TimeManager.StartCoroutine("WaitForNPCsAndInject");
#endif
        }

        /// <summary>
        /// Monitors the current state of NPCs within the game world and manages the injection of dialogue
        /// into the appropriate NPCs once they are found. This method waits for instances of NPC objects
        /// that match the specified criteria in the pending dialogue injections and processes these injections
        /// once a match is located. The method continues execution until all pending dialogue injections
        /// have been completed.
        /// </summary>
        /// <returns>
        /// An enumerator that handles the coroutine execution for periodic checks and dialogue injection processing.
        /// </returns>
        private static System.Collections.IEnumerator WaitForNPCsAndInject()
        {
            while (_pendingInjections.Count > 0)
            {
                for (int i = _pendingInjections.Count - 1; i >= 0; i--)
                {
                    DialogueInjection? injection = _pendingInjections[i];
                    NPC[]? npcs = Object.FindObjectsOfType<NPC>();
                    NPC? target = null;

                    foreach (NPC npc in npcs)
                    {
                        if (!npc || !npc.name.Contains(injection.NpcName))
                            continue;

                        target = npc;
                        break;
                    }

                    if (!target)
                        continue;

                    TryInject(injection, target);
                    _pendingInjections.RemoveAt(i);
                }

                yield return null; // Wait one frame
            }
        }

        /// <summary>
        /// Attempts to inject a dialogue choice and link into the specified NPC's dialogue system.
        /// </summary>
        /// <param name="injection">The dialogue injection object containing the data for the choice to inject.</param>
        /// <param name="npc">The NPC that will have the dialogue choice injected.</param>
        private static void TryInject(DialogueInjection injection, NPC? npc)
        {
            if (npc == null)
                return;

            DialogueHandler handler = npc.GetComponent<DialogueHandler>();
            NPCEvent_LocationDialogue dialogueEvent = npc.GetComponentInChildren<NPCEvent_LocationDialogue>(true);
            if (!dialogueEvent || !dialogueEvent.DialogueOverride)
                return;

            if (dialogueEvent.DialogueOverride.name != injection.ContainerName)
                return;

            DialogueContainer container = dialogueEvent.DialogueOverride;
            if (container.DialogueNodeData == null)
                return;

            DialogueNodeData? node = null;
            for (int i = 0; i < container.DialogueNodeData.Count; i++)
            {
                DialogueNodeData n = container.DialogueNodeData.ToArray()[i];
                if (n == null || n.Guid != injection.FromNodeGuid)
                    continue;

                node = n;
                break;
            }

            if (node == null)
                return;

            DialogueChoiceData choice = new DialogueChoiceData
            {
                Guid = System.Guid.NewGuid().ToString(),
                ChoiceLabel = injection.ChoiceLabel,
                ChoiceText = injection.ChoiceText
            };

            List<DialogueChoiceData> choiceList = new List<DialogueChoiceData>();
            if (node.choices != null)
                choiceList.AddRange(node.choices);

            choiceList.Add(choice);
            node.choices = choiceList.ToArray();

            NodeLinkData link = new NodeLinkData
            {
                BaseDialogueOrBranchNodeGuid = injection.FromNodeGuid,
                BaseChoiceOrOptionGUID = choice.Guid,
                TargetNodeGuid = injection.ToNodeGuid
            };

#if IL2CPPMELON || IL2CPPBEPINEX
            container.NodeLinks ??= new Il2CppSystem.Collections.Generic.List<NodeLinkData>();
#else
            container.NodeLinks ??= new List<NodeLinkData>();
#endif

            container.NodeLinks.Add(link);

            DialogueChoiceListener.Register(handler, injection.ChoiceLabel, injection.OnConfirmed);

            // TODO (@omar-akermi): Can you convert this to the new logger pls?
            // MelonLogger.Msg($"[DialogueInjector] Injected '{injection.ChoiceLabel}' into NPC '{npc.name}'");
        }
    }
}

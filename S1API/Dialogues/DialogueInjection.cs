using System;

namespace S1API.Dialogues
{
    /// <summary>
    /// Represents a dialogue injection configuration for adding custom dialogues into an NPC's conversation flow dynamically.
    /// </summary>
    public class DialogueInjection
    {
        /// <summary>
        /// Represents the name of the NPC (Non-Player Character) to which the dialogue injection is associated.
        /// This value is expected to match or partially match the name of an NPC in the game, allowing the system
        /// to identify and target the specific NPC for dialogue modifications.
        /// </summary>
        public string NpcName;

        /// <summary>
        /// Represents the name of the dialogue container being referenced for injections or modifications
        /// within the NPC's dialogue system.
        /// </summary>
        /// <remarks>
        /// This variable is used for identifying a specific dialogue container when attempting to
        /// inject new dialogue nodes, choices, or links into an NPC's dialogue setup.
        /// </remarks>
        public string ContainerName;

        /// <summary>
        /// Represents the unique identifier (GUID) of the starting dialogue node within a dialogue container.
        /// </summary>
        /// <remarks>
        /// This variable is used to identify the specific dialogue node from which a new choice or interaction is injected.
        /// </remarks>
        public string FromNodeGuid;

        /// <summary>
        /// Represents the unique identifier (GUID) for the target dialogue node to which a choice or link is pointing in a dialogue system.
        /// </summary>
        public string ToNodeGuid;

        /// <summary>
        /// Represents a descriptive label for a dialogue choice used in the dialogue system.
        /// </summary>
        /// <remarks>
        /// This label is utilized for identifying a specific dialogue choice during execution
        /// and for associating a callback or specific functionality when that choice is selected.
        /// </remarks>
        public string ChoiceLabel;

        /// <summary>
        /// Represents the text displayed for a dialogue choice in the game's dialogue system.
        /// </summary>
        /// <remarks>
        /// The property is utilized to define the text that appears visually for a specific dialogue choice
        /// in conjunction with the dialogue system. The text is injected dynamically during runtime for scenarios
        /// requiring additional or modified dialogue options.
        /// </remarks>
        public string ChoiceText;

        /// <summary>
        /// Represents a callback action that is invoked when a dialogue choice is confirmed.
        /// </summary>
        public Action OnConfirmed;

        /// <summary>
        /// Represents an injectable dialogue configuration that can be used to add or modify dialogue interactions in a game.
        /// </summary>
        public DialogueInjection(string npc, string container, string from, string to, string label, string text, Action onConfirmed)
        {
            NpcName = npc;
            ContainerName = container;
            FromNodeGuid = from;
            ToNodeGuid = to;
            ChoiceLabel = label;
            ChoiceText = text;
            OnConfirmed = onConfirmed;
        }
    }
}

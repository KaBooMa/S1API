using System;
using UnityEngine.Events;
#if IL2CPPBEPINEX || IL2CPPMELON
using Il2CppScheduleOne.Dialogue;
#elif MONOBEPINEX || MONOMELON
using ScheduleOne.Dialogue;
#endif

namespace S1API.Dialogues
{
    /// <summary>
    /// A static utility class that listens for and responds to specific dialogue choices in the game's dialogue system.
    /// </summary>
    public static class DialogueChoiceListener
    {
        /// <summary>
        /// Stores the label of the expected dialogue choice that, when selected,
        /// triggers the associated callback action in the dialogue system.
        /// </summary>
        /// <remarks>
        /// This variable is utilized internally by the <c>DialogueChoiceListener</c> class
        /// to match the label of the choice selected by the user. When the label matches
        /// <c>expectedChoiceLabel</c>, the registered callback is executed.
        /// </remarks>
        private static string expectedChoiceLabel;

        /// <summary>
        /// Represents a delegate invoked when a specific dialogue choice is selected during interaction.
        /// </summary>
        private static Action callback;

        /// Registers a specific dialogue choice with a callback to be invoked when the choice is selected.
        /// <param name="handlerRef">The reference to the DialogueHandler that manages dialogue choices.</param>
        /// <param name="label">The label identifying the specific dialogue choice to be registered.</param>
        /// <param name="action">The callback action to execute when the dialogue choice is selected.</param>
        public static void Register(DialogueHandler handlerRef, string label, Action action)
        {
            expectedChoiceLabel = label;
            callback = action;

            if (handlerRef != null)
            {
                void ForwardCall() => OnChoice();

                // ✅ IL2CPP-safe: explicit method binding via wrapper
                handlerRef.onDialogueChoiceChosen.AddListener((UnityAction<string>)delegate (string choice)
                {
                    if (choice == expectedChoiceLabel)
                    {
	                    ((UnityAction)ForwardCall).Invoke();
                    }
                });
            }
        }

        /// <summary>
        /// Executes the registered callback when the expected dialogue choice is selected.
        /// </summary>
        /// <remarks>
        /// This method is invoked internally and should not be called directly.
        /// It ensures that the provided callback is executed only when the expected dialogue choice matches.
        /// </remarks>
        private static void OnChoice()
        {
            callback?.Invoke();
            callback = null; // optional: remove if one-time use
        }
    }
}
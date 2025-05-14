#if (IL2CPPMELON)
using S1Quests = Il2CppScheduleOne.Quests;
using S1Variables = Il2CppScheduleOne.Variables;
using static Il2CppScheduleOne.Quests.QuestManager;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1Quests = ScheduleOne.Quests;
using S1Variables = ScheduleOne.Variables;
using static ScheduleOne.Quests.QuestManager;
#endif

using System;
using S1API.Internal.Abstraction;
using S1API.Internal.Utils;
using S1API.PhoneCalls.Constants;
using S1API.Quests;
using S1API.Quests.Constants;

namespace S1API.Conditions
{
    /// <summary>
    /// @TODO: DOCS
    /// </summary>
    public class SystemTriggerEntry
    {
        /// <summary>
        /// INTERNAL: The stored reference to the system trigger in-game.
        /// </summary>
        internal readonly S1Quests.SystemTrigger S1SystemTrigger;

        /// <summary>
        /// An action called when the <see cref="S1Quests.SystemTrigger.Conditions"/> is true
        /// </summary>
        public event Action OnEvaluateTrue
        {
            add => EventHelper.AddListener(value, S1SystemTrigger.onEvaluateTrue);
            remove => EventHelper.RemoveListener(value, S1SystemTrigger.onEvaluateTrue);
        }

        /// <summary>
        /// An action called when the <see cref="S1Quests.SystemTrigger.Conditions"/> is false
        /// </summary>
        public event Action OnEvaluateFalse
        {
            add => EventHelper.AddListener(value, S1SystemTrigger.onEvaluateFalse);
            remove => EventHelper.RemoveListener(value, S1SystemTrigger.onEvaluateFalse);
        }

        /// <summary>
        /// INTERNAL: Creates a system trigger from an in-game system trigger instance.
        /// </summary>
        /// <param name="systemTrigger"></param>
        internal SystemTriggerEntry(S1Quests.SystemTrigger systemTrigger)
        {
            S1SystemTrigger = systemTrigger;

            // Set any null values, blame runtime coding setting all of these items to null :/
            S1SystemTrigger.Conditions = new S1Variables.Conditions();
            S1SystemTrigger.Conditions.ConditionList ??= Array.Empty<S1Variables.Condition>();
            S1SystemTrigger.Conditions.QuestConditionList ??= Array.Empty<S1Variables.QuestCondition>();

            S1SystemTrigger.onEvaluateTrueVariableSetters ??= Array.Empty<S1Variables.VariableSetter>();
            S1SystemTrigger.onEvaluateFalseVariableSetters ??= Array.Empty<S1Variables.VariableSetter>();

            S1SystemTrigger.onEvaluateTrueQuestSetters ??= Array.Empty<S1Quests.QuestStateSetter>();
            S1SystemTrigger.onEvaluateFalseQuestSetters ??= Array.Empty<S1Quests.QuestStateSetter>();
        }

        /// <summary>
        /// Creates a <see cref="S1Variables.VariableSetter"/> instance
        /// </summary>
        /// <param name="evaluation">The condition to use</param>
        /// <param name="variableName">The variable to use</param>
        /// <param name="newValue">The new value for this variable</param>
        public void AddVariableSetter(EvaluationType evaluation, string variableName, string newValue)
        {
            S1Variables.VariableSetter variableSetter = new S1Variables.VariableSetter
            {
                VariableName = variableName,
                NewValue = newValue
            };

            switch (evaluation)
            {
                case EvaluationType.PassOnTrue:
                    S1SystemTrigger.onEvaluateTrueVariableSetters = S1SystemTrigger.onEvaluateTrueVariableSetters.AddItemToArray(variableSetter);
                    break;
                case EvaluationType.PassOnFalse:
                    S1SystemTrigger.onEvaluateFalseVariableSetters = S1SystemTrigger.onEvaluateFalseVariableSetters.AddItemToArray(variableSetter);
                    break;
            }
        }

        /// <summary>
        /// Creates a <see cref="S1Quests.QuestStateSetter"/> instance
        /// </summary>
        /// <param name="evaluation">The condition to use</param>
        /// <param name="questData">The <see cref="Quest"/> instance to use for this condition</param>
        /// <param name="questAction">(Optional) The state of the quest</param>
        /// <param name="questEntryState">(Optional) The state of the quest entry</param>
        public void AddQuestSetter(EvaluationType evaluation, Quest questData,
            QuestAction? questAction = null, Tuple<int, QuestState>? questEntryState = null)
        {
            S1Quests.QuestStateSetter questStateSetter = new S1Quests.QuestStateSetter
            {
                QuestName = questData.S1Quest.Title,
            };

            if (questAction.HasValue)
            {
                questStateSetter.SetQuestState = true;
                questStateSetter.QuestState = (EQuestAction)questAction.Value;
            }

            if (questEntryState != null)
            {
                questStateSetter.SetQuestEntryState = true;
                questStateSetter.QuestEntryIndex = questEntryState.Item1;
                questStateSetter.QuestEntryState = (S1Quests.EQuestState)questEntryState.Item2;
            }

            switch (evaluation)
            {
                case EvaluationType.PassOnTrue:
                    S1SystemTrigger.onEvaluateTrueQuestSetters = S1SystemTrigger.onEvaluateTrueQuestSetters.AddItemToArray(questStateSetter);
                    break;
                case EvaluationType.PassOnFalse:
                    S1SystemTrigger.onEvaluateFalseQuestSetters = S1SystemTrigger.onEvaluateFalseQuestSetters.AddItemToArray(questStateSetter);
                    break;
            }
        }

        /// <summary>
        /// Trigger the conditions for evaluation
        /// </summary>
        public void Trigger() => S1SystemTrigger.Trigger();
    }
}

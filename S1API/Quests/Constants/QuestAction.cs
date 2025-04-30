namespace S1API.Quests.Constants
{
    /// <summary>
    /// A wrapper around EQuestAction
    /// </summary>
    public enum QuestAction
    {
        /// <summary>
        /// Begin the quest
        /// </summary>
        Begin,

        /// <summary>
        /// Succeed (complete) the quest
        /// </summary>
        Success,

        /// <summary>
        /// Fail the quest
        /// </summary>
        Fail,

        /// <summary>
        /// Expire the quest
        /// </summary>
        Expire,

        /// <summary>
        /// Cancel the quest
        /// </summary>
        Cancel
    }
}

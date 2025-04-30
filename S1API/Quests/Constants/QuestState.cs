namespace S1API.Quests.Constants
{
    /// <summary>
    /// Represents all states a quest can be. Applicable to quest entries as well.
    /// </summary>
    public enum QuestState
    {
        /// <summary>
        /// Represents a quest / quest entry that has not been started yet.
        /// </summary>
        Inactive,

        /// <summary>
        /// Represents a quest / quest entry that has been started but not ended.
        /// </summary>
        Active,

        /// <summary>
        /// Represents a quest / quest entry that has been completed successfully by the player.
        /// </summary>
        Completed,

        /// <summary>
        /// Represents a quest / quest entry that has been failed by the played.
        /// </summary>
        Failed,

        /// <summary>
        /// Represents a quest / quest entry that has been expired.
        /// </summary>
        Expired,

        /// <summary>
        /// Represents a quest / quest entry that has been cancelled.
        /// </summary>
        Cancelled
    }
}

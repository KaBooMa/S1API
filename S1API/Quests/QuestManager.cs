using System;
using System.Linq;

namespace S1API.Quests
{
    /// <summary>
    /// Provided management of quests across the game.
    /// </summary>
    public static class QuestManager
    {
        /// <summary>
        /// INTERNAL: Tracking of all custom quests.
        /// </summary>
        internal static readonly System.Collections.Generic.List<Quest> Quests = new System.Collections.Generic.List<Quest>();

        /// <summary>
        /// Creates a new quest for the player to complete from your custom quest class.
        /// </summary>
        /// <param name="guid">The unique identifier for this quest. By default, assigned a random GUID.</param>
        /// <typeparam name="T">Your custom quest class that derived from <see cref="Quest"/>.</typeparam>
        /// <returns>The instance of your quest.</returns>
        public static Quest CreateQuest<T>(string? guid = null) where T : Quest =>
            CreateQuest(typeof(T), guid);

        /// <summary>
        /// Creates a new quest for the player to complete from your custom quest class.
        /// </summary>
        /// <param name="questType">Your custom quest class that derived from <see cref="Quest"/>.</param>
        /// <param name="guid">The unique identifier for this quest. By default, assigned a random GUID.</param>
        /// <returns></returns>
        public static Quest CreateQuest(Type questType, string? guid = null)
        {
            Quest? quest = (Quest)Activator.CreateInstance(questType)!;
            if (quest == null)
            {
	            throw new Exception($"Unable to create quest instance of {questType.FullName}!");
            }

            Quests.Add(quest);
            return quest;
        }

        /// <summary>
        /// Returns a <see cref="Quest"/> instance by the Quest GUID
        /// </summary>
        /// <param name="guid">The unique identifier to use for searching this quest</param>
        /// <returns>The quest instance</returns>
        public static Quest? GetQuestByGuid(string guid)
        {
            return Quests.FirstOrDefault(x => x.S1Quest.StaticGUID == guid);
        }

        /// <summary>
        /// Returns a <see cref="Quest"/> instance by the Quest Name
        /// </summary>
        /// <param name="questName">The quest name to use for searching this quest</param>
        /// <returns>The quest instance</returns>
        public static Quest? GetQuestByName(string questName)
        {
            return Quests.FirstOrDefault(x => x.S1Quest.Title == questName);
        }
    }
}

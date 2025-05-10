#if (IL2CPPMELON)
using S1Loaders = Il2CppScheduleOne.Persistence.Loaders;
using S1Datas = Il2CppScheduleOne.Persistence.Datas;
using S1Quests = Il2CppScheduleOne.Quests;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1Loaders = ScheduleOne.Persistence.Loaders;
using S1Datas = ScheduleOne.Persistence.Datas;
using S1Quests = ScheduleOne.Quests;
#endif

#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppSystem.Collections.Generic;
#elif (MONOMELON || MONOBEPINEX)
using System.Collections.Generic;
#endif

using System;
using System.IO;
using System.Linq;
using HarmonyLib;
using Newtonsoft.Json;
using S1API.Internal.Abstraction;
using S1API.Internal.Utils;
using S1API.Quests;
using UnityEngine;

namespace S1API.Internal.Patches
{
    /// <summary>
    /// INTERNAL: All patches related to quests.
    /// </summary>
    [HarmonyPatch]
    internal class QuestPatches
    {
        /// <summary>
        /// Patching performed when all quests are saved.
        /// </summary>
        /// <param name="__instance">Instance of the quest manager.</param>
        /// <param name="parentFolderPath">Path to the base Quest folder.</param>
        /// <param name="__result">List of extra saveable data. The game uses this for cleanup later.</param>
        [HarmonyPatch(typeof(S1Quests.QuestManager), "WriteData")]
        [HarmonyPostfix]
        private static void QuestManagerWriteData(S1Quests.QuestManager __instance, string parentFolderPath, ref List<string> __result)
        {
            string questsPath = Path.Combine(parentFolderPath, "Quests");

            foreach (Quest quest in QuestManager.Quests)
                quest.SaveInternal(questsPath, ref __result);
        }

        /// <summary>
        /// Patching performed for when all quests are loaded.
        /// </summary>
        /// <param name="__instance">Instance of the quest loader.</param>
        /// <param name="mainPath">Path to the base Quest folder.</param>
        [HarmonyPatch(typeof(S1Loaders.QuestsLoader), "Load")]
        [HarmonyPostfix]
        private static void QuestsLoaderLoad(S1Loaders.QuestsLoader __instance, string mainPath)
        {
            // Make sure we have a quests directory (fresh saves don't at this point in runtime)
            if (!Directory.Exists(mainPath))
                return;

            string[] questDirectories = Directory.GetDirectories(mainPath)
                .Select(Path.GetFileName)
                .Where(directory => directory != null && directory.StartsWith("Quest_"))
                .ToArray()!;

            foreach (string questDirectory in questDirectories)
            {
                string baseQuestPath = Path.Combine(mainPath, questDirectory);
                __instance.TryLoadFile(baseQuestPath, out string questDataText);
                if (questDataText == null)
                    continue;

                S1Datas.QuestData baseQuestData = JsonUtility.FromJson<S1Datas.QuestData>(questDataText);

                string questDirectoryPath = Path.Combine(mainPath, questDirectory);
                string questDataPath = Path.Combine(questDirectoryPath, "QuestData");
                if (!__instance.TryLoadFile(questDataPath, out string questText))
                    continue;

                QuestData? questData = JsonConvert.DeserializeObject<QuestData>(questText, ISaveable.SerializerSettings);
                if (questData?.ClassName == null)
                    continue;

                Type? questType = ReflectionUtils.GetTypeByName(questData.ClassName);
                if (questType == null || !typeof(Quest).IsAssignableFrom(questType))
                    continue;

                Quest quest = QuestManager.CreateQuest(questType, baseQuestData?.GUID);
                quest.LoadInternal(questDirectoryPath);
            }
        }

        /// <summary>
        /// Patching performed for when stale files are deleted.
        /// </summary>
        /// <param name="__instance">Instance of the quest manager.</param>
        /// <param name="parentFolderPath">Path to the base Quest folder.</param>
        [HarmonyPatch(typeof(S1Quests.QuestManager), "DeleteUnapprovedFiles")]
        [HarmonyPostfix]
        private static void QuestManagerDeleteUnapprovedFiles(S1Quests.QuestManager __instance, string parentFolderPath)
        {
            string questFolder = Path.Combine(parentFolderPath, "Quests");
            string?[] existingQuests = QuestManager.Quests.Select(quest => quest.SaveFolder).ToArray();

            string[] unapprovedQuestDirectories = Directory.GetDirectories(questFolder)
                .Where(directory => directory.StartsWith("Quest_") && !existingQuests.Contains(directory))
                .ToArray();

            foreach (string unapprovedQuestDirectory in unapprovedQuestDirectories)
                Directory.Delete(unapprovedQuestDirectory, true);
        }

        [HarmonyPatch(typeof(S1Quests.Quest), "Start")]
        [HarmonyPrefix]
        private static void QuestStart(S1Quests.Quest __instance) =>
            QuestManager.Quests.FirstOrDefault(quest => quest.S1Quest == __instance)?.CreateInternal();

        /////// TODO: Quests doesn't have OnDestroy. Find another way to clean up
        // [HarmonyPatch(typeof(S1Quests.Quest), "OnDestroy")]
        // [HarmonyPostfix]
        // private static void NPCOnDestroy(S1NPCs.NPC __instance)
        // {
        //     NPCs.RemoveAll(npc => npc.S1NPC == __instance);
        //     NPC? npc = NPCs.FirstOrDefault(npc => npc.S1NPC == __instance);
        //     if (npc == null)
        //         return;
        //
        //     // npc.OnDestroyed();
        //     NPCs.Remove(npc);
        // }
    }
}

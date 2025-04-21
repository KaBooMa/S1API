#if (IL2CPP)
using S1Quests = Il2CppScheduleOne.Quests;
using S1Dev = Il2CppScheduleOne.DevUtilities;
using S1Map = Il2CppScheduleOne.Map;
using S1Data = Il2CppScheduleOne.Persistence.Datas;
using S1Contacts = Il2CppScheduleOne.UI.Phone.ContactsApp;
using Il2CppSystem.Collections.Generic;
#elif (MONO)
using S1Quests = ScheduleOne.Quests;
using S1Dev = ScheduleOne.DevUtilities;
using S1Map = ScheduleOne.Map;
using S1Data = ScheduleOne.Persistence.Datas;
using S1Contacts = ScheduleOne.UI.Phone.ContactsApp;
using System.Reflection;
using System.Collections.Generic;
#endif

using System;
using System.IO;
using HarmonyLib;
using S1API.Internal.Abstraction;
using S1API.Internal.Utils;
using S1API.Saveables;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace S1API.Quests
{
    /// <summary>
    /// An abstract class intended to be derived from for creating custom quests in the game.
    /// </summary>
    public abstract class Quest : Saveable
    {
        /// <summary>
        /// The title of the quest to display for the player.
        /// </summary>
        protected abstract string Title { get; }
        
        /// <summary>
        /// The description provided to the player.
        /// </summary>
        protected abstract string Description { get; }
        
        /// <summary>
        /// Whether to automatically begin the quest once instanced.
        /// NOTE: If this is false, you must manually `.Begin()` this quest.
        /// </summary>
        protected virtual bool AutoBegin => true;
        
        // ReSharper disable once MemberCanBePrivate.Global
        /// <summary>
        /// A list of all quest entries added to this quest.
        /// </summary>
        protected readonly QuestEntry[] QuestEntries = Array.Empty<QuestEntry>();
        
        [SaveableField("QuestData")] 
        private readonly QuestData _questData;
        
        internal string? SaveFolder => S1Quest.SaveFolderName;
        /// <summary>
        /// Optional icon sprite to display for the quest. 
        /// Override to use a custom icon loaded at runtime (e.g., from a file).
        /// protected override Sprite? QuestIcon => ImageUtils.LoadImage("icon.png");
        /// </summary>
        protected virtual Sprite? QuestIcon => null;

        internal readonly S1Quests.Quest S1Quest;
        private readonly GameObject _gameObject;
        
        /// <summary>
        /// INTERNAL: Public constructor used for instancing the quest.
        /// </summary>
        public Quest()
        {
            _questData = new QuestData(GetType().Name);
            
            _gameObject = new GameObject("Quest");
            S1Quest = _gameObject.AddComponent<S1Quests.Quest>();
            S1Quest.StaticGUID = string.Empty;
            S1Quest.onActiveState = new UnityEvent();
            S1Quest.onComplete = new UnityEvent();
            S1Quest.onInitialComplete = new UnityEvent();
            S1Quest.onQuestBegin = new UnityEvent();
            S1Quest.onQuestEnd = new UnityEvent<S1Quests.EQuestState>();
            S1Quest.onTrackChange = new UnityEvent<bool>();
            S1Quest.TrackOnBegin = true;
            S1Quest.AutoCompleteOnAllEntriesComplete = true;
#if (MONO)
            FieldInfo autoInitField = AccessTools.Field(typeof(S1Quests.Quest), "autoInitialize");
            autoInitField.SetValue(S1Quest, false);
#elif (IL2CPP)
            S1Quest.autoInitialize = false;
#endif
            
            // Setup quest icon prefab
            GameObject iconPrefabObject = new GameObject("IconPrefab", 
                CrossType.Of<RectTransform>(), 
                CrossType.Of<CanvasRenderer>(), 
                CrossType.Of<Image>()
            );
            iconPrefabObject.transform.SetParent(_gameObject.transform);
            Image iconImage = iconPrefabObject.GetComponent<Image>();
            iconImage.sprite = QuestIcon ?? S1Dev.PlayerSingleton<S1Contacts.ContactsApp>.Instance.AppIcon;
            S1Quest.IconPrefab = iconPrefabObject.GetComponent<RectTransform>();
            
            // Setup UI for POI prefab
            var uiPrefabObject = new GameObject("PoIUIPrefab",
                CrossType.Of<RectTransform>(), 
                CrossType.Of<CanvasRenderer>(), 
                CrossType.Of<EventTrigger>(),
                CrossType.Of<Button>()
            );
            uiPrefabObject.transform.SetParent(_gameObject.transform);

            var labelObject = new GameObject("MainLabel",
                CrossType.Of<RectTransform>(), 
                CrossType.Of<CanvasRenderer>(), 
                CrossType.Of<Text>()
            );
            labelObject.transform.SetParent(uiPrefabObject.transform);

            var iconContainerObject = new GameObject("IconContainer",
                CrossType.Of<RectTransform>(), 
                CrossType.Of<CanvasRenderer>(), 
                CrossType.Of<Image>()
            );
            iconContainerObject.transform.SetParent(uiPrefabObject.transform);
            Image poiIconImage = iconContainerObject.GetComponent<Image>();
            poiIconImage.sprite = QuestIcon ?? S1Dev.PlayerSingleton<S1Contacts.ContactsApp>.Instance.AppIcon;
            RectTransform iconRectTransform = poiIconImage.GetComponent<RectTransform>();
            iconRectTransform.sizeDelta = new Vector2(20, 20);
            
            // Setup POI prefab
            GameObject poiPrefabObject = new GameObject("POIPrefab");
            poiPrefabObject.SetActive(false);
            poiPrefabObject.transform.SetParent(_gameObject.transform);
            S1Map.POI poi = poiPrefabObject.AddComponent<S1Map.POI>();
            poi.DefaultMainText = "Did it work?";
#if (MONO)
            FieldInfo uiPrefabField = AccessTools.Field(typeof(S1Map.POI), "UIPrefab");
            uiPrefabField.SetValue(poi, uiPrefabObject);
#elif (IL2CPP)
            poi.UIPrefab = uiPrefabObject;
#endif
            S1Quest.PoIPrefab = poiPrefabObject;
        }

        /// <summary>
        /// INTERNAL: Delayed initialization of the quest.
        /// This allows the base game to get things setup beforehand.
        /// </summary>
        internal override void CreateInternal()
        {
            
            base.CreateInternal();
            
            // Initialize the quest
            S1Quest.InitializeQuest(Title, Description, Array.Empty<S1Data.QuestEntryData>(), S1Quest?.StaticGUID);
            
            if (AutoBegin)
                S1Quest?.Begin();
        }

        internal override void SaveInternal(string folderPath, ref List<string> extraSaveables)
        {
            string questDataPath = Path.Combine(folderPath, S1Quest.SaveFolderName);
            if (!Directory.Exists(questDataPath))
                Directory.CreateDirectory(questDataPath);
            
            base.SaveInternal(questDataPath, ref extraSaveables);
        }

        /// <summary>
        /// Adds a new quest entry to the quest.
        /// </summary>
        /// <param name="title">The title for the quest entry.</param>
        /// <param name="poiPosition">A position for the point-of-interest, if applicable.</param>
        /// <returns>A reference to the quest entry</returns>
        protected QuestEntry AddEntry(string title, Vector3? poiPosition = null)
        {
            var questEntryObject = new GameObject($"QuestEntry");
            questEntryObject.transform.SetParent(_gameObject?.transform);
            
            S1Quests.QuestEntry s1QuestEntry = questEntryObject.AddComponent<S1Quests.QuestEntry>();
            s1QuestEntry.PoILocation = questEntryObject.transform;
            S1Quest.Entries.Add(s1QuestEntry);
            
            QuestEntry questEntry = new QuestEntry(s1QuestEntry)
            {
                Title = title,
                POIPosition = poiPosition ?? Vector3.zero
            };
            QuestEntries.AddItem(questEntry);
            
            return questEntry;
        }
        
        /// <summary>
        /// Starts the quest for the save file.
        /// </summary>
        public void Begin() => S1Quest?.Begin();
        
        /// <summary>
        /// Cancels the quest for the save file.
        /// </summary>
        public void Cancel() => S1Quest?.Cancel();
        
        /// <summary>
        /// Expires the quest for the save file.
        /// </summary>
        public void Expire() => S1Quest?.Expire();
        
        /// <summary>
        /// Fails the quest for the save file.
        /// </summary>
        public void Fail() => S1Quest?.Fail();
        
        /// <summary>
        /// Completes the quest for the save file.
        /// </summary>
        public void Complete() => S1Quest?.Complete();
        
        /// <summary>
        /// Ends the quest for the save file.
        /// NOTE: This is done upon completion of the entries by default.
        /// </summary>
        public void End() => S1Quest?.End();
    }
}
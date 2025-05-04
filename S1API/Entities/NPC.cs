#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1DevUtilities = Il2CppScheduleOne.DevUtilities;
using S1Interaction = Il2CppScheduleOne.Interaction;
using S1Messaging = Il2CppScheduleOne.Messaging;
using S1Noise = Il2CppScheduleOne.Noise;
using S1Relation = Il2CppScheduleOne.NPCs.Relation;
using S1Responses = Il2CppScheduleOne.NPCs.Responses;
using S1PlayerScripts = Il2CppScheduleOne.PlayerScripts;
using S1ContactApps = Il2CppScheduleOne.UI.Phone.ContactsApp;
using S1WorkspacePopup = Il2CppScheduleOne.UI.WorldspacePopup;
using S1AvatarFramework = Il2CppScheduleOne.AvatarFramework;
using S1Behaviour = Il2CppScheduleOne.NPCs.Behaviour;
using S1Vehicles = Il2CppScheduleOne.Vehicles;
using S1Vision = Il2CppScheduleOne.Vision;
using S1NPCs = Il2CppScheduleOne.NPCs;
#elif (MONOMELON || MONOBEPINEX)
using S1DevUtilities = ScheduleOne.DevUtilities;
using S1Interaction = ScheduleOne.Interaction;
using S1Messaging = ScheduleOne.Messaging;
using S1Noise = ScheduleOne.Noise;
using S1Relation = ScheduleOne.NPCs.Relation;
using S1Responses = ScheduleOne.NPCs.Responses;
using S1PlayerScripts = ScheduleOne.PlayerScripts;
using S1ContactApps = ScheduleOne.UI.Phone.ContactsApp;
using S1WorkspacePopup = ScheduleOne.UI.WorldspacePopup;
using S1AvatarFramework = ScheduleOne.AvatarFramework;
using S1Behaviour = ScheduleOne.NPCs.Behaviour;
using S1Vehicles = ScheduleOne.Vehicles;
using S1Vision = ScheduleOne.Vision;
using S1NPCs = ScheduleOne.NPCs;
#endif

#if (IL2CPPBEPINEX || IL2CPPMELON)
using Il2CppSystem.Collections.Generic;
#else
using System.Collections.Generic;
#endif

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using S1API.Entities.Interfaces;
using S1API.Internal.Abstraction;
using S1API.Map;
using S1API.Messaging;
using UnityEngine;
using UnityEngine.Events;

namespace S1API.Entities
{
    /// <summary>
    /// An abstract class intended to be derived from for creating custom NPCs in the game.
    /// </summary>
#pragma warning disable S101
    public abstract class NPC : Saveable, IEntity, IHealth
#pragma warning restore S101
    {
        // Protected members intended to be used by modders.
        // Intended to be used from within the class / derived classes ONLY.
        #region Protected Members

        /// <summary>
        /// A list of text responses you've added to your NPC.
        /// </summary>
        protected readonly System.Collections.Generic.List<Response> Responses = new System.Collections.Generic.List<Response>();

        /// <summary>
        /// Base constructor for a new NPC.
        /// Not intended for instancing your NPC!
        /// Instead, create your derived class and let S1API handle instancing.
        /// </summary>
        /// <param name="id">Unique identifier for your NPC.</param>
        /// <param name="firstName">The first name for your NPC.</param>
        /// <param name="lastName">The last name for your NPC.</param>
        /// <param name="icon">The icon for your NPC for messages, realationships, etc.</param>
        protected NPC(
            string id,
            string? firstName,
            string? lastName,
            Sprite? icon = null
            )
        {
            IsCustomNPC = true;
            gameObject = new GameObject();

            // Deactivate game object til we're done
            gameObject.SetActive(false);

            // Setup the base NPC class
            S1NPC = gameObject.AddComponent<S1NPCs.NPC>();
            S1NPC.FirstName = firstName;
            S1NPC.LastName = lastName;
            S1NPC.ID = id;
            S1NPC.MugshotSprite = icon ?? S1DevUtilities.PlayerSingleton<S1ContactApps.ContactsApp>.Instance.AppIcon;
            S1NPC.BakedGUID = Guid.NewGuid().ToString();

            // ReSharper disable once UseObjectOrCollectionInitializer (IL2CPP COMPAT)
            S1NPC.ConversationCategories = new List<S1Messaging.EConversationCategory>();
            S1NPC.ConversationCategories.Add(S1Messaging.EConversationCategory.Customer);

            // Create our MessageConversation
#if (IL2CPPMELON || IL2CPPBEPINEX)
            S1NPC.CreateMessageConversation();
#elif (MONOMELON || MONOBEPINEX)
            MethodInfo createConvoMethod = AccessTools.Method(typeof(S1NPCs.NPC), "CreateMessageConversation");
            createConvoMethod.Invoke(S1NPC, null);
#endif

            // Add UnityEvents for NPCHealth
            S1NPC.Health = gameObject.GetComponent<S1NPCs.NPCHealth>();
            S1NPC.Health.onDie = new UnityEvent();
            S1NPC.Health.onKnockedOut = new UnityEvent();
            S1NPC.Health.Invincible = true;
            S1NPC.Health.MaxHealth = 100f;

            // Awareness behaviour
            GameObject awarenessObject = new GameObject("NPCAwareness");
            awarenessObject.transform.SetParent(gameObject.transform);
            S1NPC.awareness = awarenessObject.AddComponent<S1NPCs.NPCAwareness>();
            S1NPC.awareness.onExplosionHeard = new UnityEvent<S1Noise.NoiseEvent>();
            S1NPC.awareness.onGunshotHeard = new UnityEvent<S1Noise.NoiseEvent>();
            S1NPC.awareness.onHitByCar = new UnityEvent<S1Vehicles.LandVehicle>();
            S1NPC.awareness.onNoticedDrugDealing = new UnityEvent<S1PlayerScripts.Player>();
            S1NPC.awareness.onNoticedGeneralCrime = new UnityEvent<S1PlayerScripts.Player>();
            S1NPC.awareness.onNoticedPettyCrime = new UnityEvent<S1PlayerScripts.Player>();
            S1NPC.awareness.onNoticedPlayerViolatingCurfew = new UnityEvent<S1PlayerScripts.Player>();
            S1NPC.awareness.onNoticedSuspiciousPlayer = new UnityEvent<S1PlayerScripts.Player>();
            S1NPC.awareness.Listener = gameObject.AddComponent<S1Noise.Listener>();

            /////// START BEHAVIOUR CODE ////////
            // NPCBehaviours behaviour
            GameObject behaviourObject = new GameObject("NPCBehaviour");
            behaviourObject.transform.SetParent(gameObject.transform);
            S1Behaviour.NPCBehaviour behaviour = behaviourObject.AddComponent<S1Behaviour.NPCBehaviour>();

            GameObject cowingBehaviourObject = new GameObject("CowingBehaviour");
            cowingBehaviourObject.transform.SetParent(behaviourObject.transform);
            S1Behaviour.CoweringBehaviour coweringBehaviour = cowingBehaviourObject.AddComponent<S1Behaviour.CoweringBehaviour>();

            GameObject fleeBehaviourObject = new GameObject("FleeBehaviour");
            fleeBehaviourObject.transform.SetParent(behaviourObject.transform);
            S1Behaviour.FleeBehaviour fleeBehaviour = fleeBehaviourObject.AddComponent<S1Behaviour.FleeBehaviour>();

            behaviour.CoweringBehaviour = coweringBehaviour;
            behaviour.FleeBehaviour = fleeBehaviour;
            S1NPC.behaviour = behaviour;
            /////// END BEHAVIOUR CODE ////////

            // Response to actions like gunshots, drug deals, etc.
            GameObject responsesObject = new GameObject("NPCResponses");
            responsesObject.transform.SetParent(gameObject.transform);
            S1NPC.awareness.Responses = responsesObject.AddComponent<S1Responses.NPCResponses_Civilian>();

            // Vision cone object and behaviour
            GameObject visionObject = new GameObject("VisionCone");
            visionObject.transform.SetParent(gameObject.transform);
            S1Vision.VisionCone visionCone = visionObject.AddComponent<S1Vision.VisionCone>();
            visionCone.StatesOfInterest.Add(new S1Vision.VisionCone.StateContainer
            {
                state = S1PlayerScripts.PlayerVisualState.EVisualState.PettyCrime, RequiredNoticeTime = 0.1f
            });
            S1NPC.awareness.VisionCone = visionCone;


            // Suspicious ? icon in world space
            S1NPC.awareness.VisionCone.QuestionMarkPopup = gameObject.AddComponent<S1WorkspacePopup.WorldspacePopup>();

            // Interaction behaviour
#if (IL2CPPMELON || IL2CPPBEPINEX)
            S1NPC.intObj = gameObject.AddComponent<S1Interaction.InteractableObject>();
#elif (MONOMELON || MONOBEPINEX)
            FieldInfo intObjField = AccessTools.Field(typeof(S1NPCs.NPC), "intObj");
            intObjField.SetValue(S1NPC, gameObject.AddComponent<S1Interaction.InteractableObject>());
#endif

            // Relationship data
            S1NPC.RelationData = new S1Relation.NPCRelationData();

            // Inventory behaviour
            S1NPCs.NPCInventory inventory = gameObject.AddComponent<S1NPCs.NPCInventory>();

            // Pickpocket behaviour
            inventory.PickpocketIntObj = gameObject.AddComponent<S1Interaction.InteractableObject>();

            // Defaulting to the local player for Avatar TODO: Change
            S1NPC.Avatar = S1AvatarFramework.MugshotGenerator.Instance.MugshotRig;

            // Enable our custom gameObjects so they can initialize
            gameObject.SetActive(true);

            All.Add(this);
        }

        /// <summary>
        /// Called when a response is loaded from the save file.
        /// Override this method for attaching your callbacks to your methods.
        /// </summary>
        /// <param name="response">The response that was loaded.</param>
        protected virtual void OnResponseLoaded(Response response) { }

        #endregion

        // Public members intended to be used by modders.
        // Can be used inside your derived class, or outside via instance reference.
        #region Public Members

        /// <summary>
        /// INTERNAL: Tracking for the GameObject associated with this NPC.
        /// Not intended for use by modders!
        /// </summary>
        public GameObject gameObject { get; }

        /// <summary>
        /// The world position of the NPC.
        /// </summary>
        public Vector3 Position
        {
            get => gameObject.transform.position;
            set => S1NPC.Movement.Warp(value);
        }

        /// <summary>
        /// The transform of the NPC.
        /// Please do not set the properties of this transform.
        /// </summary>
        public Transform Transform =>
            gameObject.transform;

        /// <summary>
        /// List of all NPCs within the base game and modded.
        /// </summary>
#pragma warning disable S3887
#pragma warning disable S2386
        public static readonly System.Collections.Generic.List<NPC> All = new System.Collections.Generic.List<NPC>();
#pragma warning restore S2386
#pragma warning restore S3887

        /// <summary>
        /// The first name of this NPC.
        /// </summary>
        public string FirstName
        {
            get => S1NPC.FirstName;
            set => S1NPC.FirstName = value;
        }

        /// <summary>
        /// The last name of this NPC.
        /// </summary>
        public string LastName
        {
            get => S1NPC.LastName;
            set => S1NPC.LastName = value;
        }

        /// <summary>
        /// The full name of this NPC.
        /// If there is no last name, it will just return the first name.
        /// </summary>
        public string FullName =>
            S1NPC.fullName;

        /// <summary>
        /// The unique identifier to assign to this NPC.
        /// Used when saving and loading. Probably other things within the base game code.
        /// </summary>
        public string ID
        {
            get => S1NPC.ID;
            protected set => S1NPC.ID = value;
        }

        /// <summary>
        /// The icon assigned to this NPC.
        /// </summary>
        public Sprite Icon
        {
            get => S1NPC.MugshotSprite;
            set => S1NPC.MugshotSprite = value;
        }

        /// <summary>
        /// Whether the NPC is currently conscious or not.
        /// </summary>
        public bool IsConscious =>
            S1NPC.IsConscious;

        /// <summary>
        /// Whether the NPC is currently inside a building or not.
        /// </summary>
        public bool IsInBuilding =>
            S1NPC.isInBuilding;

        /// <summary>
        /// Whether the NPC is currently inside a vehicle or not.
        /// </summary>
        public bool IsInVehicle =>
            S1NPC.IsInVehicle;

        /// <summary>
        /// Whether the NPC is currently panicking or not.
        /// </summary>
        public bool IsPanicking =>
            S1NPC.IsPanicked;

        /// <summary>
        /// Whether the NPC is currently unsettled or not.
        /// </summary>
        public bool IsUnsettled =>
            S1NPC.isUnsettled;

        /// <summary>
        /// UNCONFIRMED: Whether the NPC is currently visible to the player or not.
        /// If you confirm this, please let us know so we can update the documentation!
        /// </summary>
        public bool IsVisible =>
            S1NPC.isVisible;

        /// <summary>
        /// How aggressive this NPC is towards others.
        /// </summary>
        public float Aggressiveness
        {
            get => S1NPC.Aggression;
            set => S1NPC.Aggression = value;
        }

        /// <summary>
        /// The region the NPC is associated with.
        /// Note: Not the region they're in currently. Just the region they're designated to.
        /// </summary>
        public Region Region =>
            (Region)S1NPC.Region;

        /// <summary>
        /// UNCONFIRMED: How long the NPC will panic for.
        /// If you confirm this, please let us know so we can update the documentation!
        /// </summary>
        public float PanicDuration
        {
            get => (float)_panicField.GetValue(S1NPC)!;
            set => _panicField.SetValue(S1NPC, value);
        }

        /// <summary>
        /// Sets the scale of the NPC.
        /// </summary>
        public float Scale
        {
            get => S1NPC.Scale;
            set => S1NPC.SetScale(value);
        }

        /// <summary>
        /// Whether the NPC is knocked out or not.
        /// </summary>
        public bool IsKnockedOut =>
            S1NPC.Health.IsKnockedOut;

        /// <summary>
        /// UNCONFIRMED: Whether the NPC requires the region unlocked in order to deal to.
        /// If you confirm this, please let us know so we can update the documentation!
        /// </summary>
        public bool RequiresRegionUnlocked
        {
            get => (bool)_requiresRegionUnlockedField.GetValue(S1NPC)!;
            set => _panicField.SetValue(S1NPC, value);
        }

        // TODO: Add CurrentBuilding (currently missing NPCEnterableBuilding abstraction)
        // public ??? CurrentBuilding { get; set; }

        // TODO: Add CurrentVehicle (currently missing LandVehicle abstraction)
        // public ??? CurrentVehicle { get; set; }

        // TODO: Add Inventory (currently missing NPCInventory abstraction)
        // public ??? Inventory { get; set; }

        /// <summary>
        /// The current health the NPC has.
        /// </summary>
        public float CurrentHealth =>
            S1NPC.Health.Health;

        /// <summary>
        /// The maximum health the NPC has.
        /// </summary>
        public float MaxHealth
        {
            get => S1NPC.Health.MaxHealth;
            set => S1NPC.Health.MaxHealth = value;
        }

        /// <summary>
        /// Whether the NPC is dead or not.
        /// </summary>
        public bool IsDead =>
            S1NPC.Health.IsDead;

        /// <summary>
        /// Whether the NPC is invincible or not.
        /// </summary>
        public bool IsInvincible
        {
            get => S1NPC.Health.Invincible;
            set => S1NPC.Health.Invincible = value;
        }

        /// <summary>
        /// Revives the NPC.
        /// </summary>
        public void Revive() =>
            S1NPC.Health.Revive();

        /// <summary>
        /// Deals damage to the NPC.
        /// </summary>
        /// <param name="amount">The amount of damage to deal.</param>
        public void Damage(int amount)
        {
            if (amount <= 0)
            {
	            return;
            }

            S1NPC.Health.TakeDamage(amount, true);
        }

        /// <summary>
        ///  Heals the NPC.
        /// </summary>
        /// <param name="amount">The amount of health to heal.</param>
        public void Heal(int amount)
        {
            if (amount <= 0)
            {
	            return;
            }

            float actualHealAmount = Mathf.Min(amount, S1NPC.Health.MaxHealth - S1NPC.Health.Health);
            S1NPC.Health.TakeDamage(-actualHealAmount, false);
        }

        /// <summary>
        /// Kills the NPC.
        /// </summary>
        public void Kill() =>
            S1NPC.Health.TakeDamage(S1NPC.Health.MaxHealth);

        /// <summary>
        /// Causes the NPC to become unsettled.
        /// UNCONFIRMED: Will panic them for PanicDuration amount of time.
        /// If you confirm this, please let us know so we can update the documentation!
        /// </summary>
        /// <param name="duration">Length of time they should stay unsettled.</param>
        public void Unsettle(float duration) =>
            _unsettleMethod.Invoke(S1NPC, new object[] { duration });

        /// <summary>
        /// Smoothly scales the NPC over lerpTime.
        /// </summary>
        /// <param name="scale">The scale you want set.</param>
        /// <param name="lerpTime">The time to scale over.</param>
        public void LerpScale(float scale, float lerpTime) =>
            S1NPC.SetScale(scale, lerpTime);

        /// <summary>
        /// Causes the NPC to become panicked.
        /// </summary>
        public void Panic() =>
            S1NPC.SetPanicked();

        /// <summary>
        /// Causes the NPC to stop panicking, if they are currently.
        /// </summary>
        public void StopPanicking() =>
            _removePanicMethod.Invoke(S1NPC, new object[] { });

        /// <summary>
        /// Knocks the NPC out.
        /// NOTE: Does not work for invincible NPCs.
        /// </summary>
        public void KnockOut() =>
            S1NPC.Health.KnockOut();

        /// <summary>
        /// Tells the NPC to travel to a specific position in world space.
        /// </summary>
        /// <param name="position">The position to travel to.</param>
        public void Goto(Vector3 position) =>
            S1NPC.Movement.SetDestination(position);

        // TODO: Add OnEnterVehicle listener (currently missing LandVehicle abstraction)
        // public event Action OnEnterVehicle { }

        // TODO: Add OnExitVehicle listener (currently missing LandVehicle abstraction)
        // public event Action OnExitVehicle { }

        // TODO: Add OnExplosionHeard listener (currently missing NoiseEvent abstraction)
        // public event Action OnExplosionHeard { }

        // TODO: Add OnGunshotHeard listener (currently missing NoiseEvent abstraction)
        // public event Action OnGunshotHeard { }

        // TODO: Add OnHitByCar listener (currently missing LandVehicle abstraction)
        // public event Action OnHitByCar { }

        // TODO: Add OnNoticedDrugDealing listener (currently missing Player abstraction)
        // public event Action OnNoticedDrugDealing { }

        // TODO: Add OnNoticedGeneralCrime listener (currently missing Player abstraction)
        // public event Action OnNoticedGeneralCrime { }

        // TODO: Add OnNoticedPettyCrime listener (currently missing Player abstraction)
        // public event Action OnNoticedPettyCrime { }

        // TODO: Add OnPlayerViolatingCurfew listener (currently missing Player abstraction)
        // public event Action OnPlayerViolatingCurfew { }

        // TODO: Add OnNoticedSuspiciousPlayer listener (currently missing Player abstraction)
        // public event Action OnNoticedSuspiciousPlayer { }

        /// <summary>
        /// Called when the NPC died.
        /// </summary>
        public event Action OnDeath
        {
            add => EventHelper.AddListener(value, S1NPC.Health.onDie);
            remove => EventHelper.RemoveListener(value, S1NPC.Health.onDie);
        }

        /// <summary>
        /// Called when the NPC's inventory contents change.
        /// </summary>
        public event Action OnInventoryChanged
        {
            add => EventHelper.AddListener(value, S1NPC.Inventory.onContentsChanged);
            remove => EventHelper.RemoveListener(value, S1NPC.Inventory.onContentsChanged);
        }

        /// <summary>
        /// Sends a text message from this NPC to the players.
        /// Supports responses with callbacks for additional logic.
        /// </summary>
        /// <param name="message">The message you want the player to see. Unity rich text is allowed.</param>
        /// <param name="responses">Instances of <see cref="Response"/> to display.</param>
        /// <param name="responseDelay">The delay between when the message is sent and when the player can reply.</param>
        /// <param name="network">Whether this should propagate to all players or not.</param>
        public void SendTextMessage(string message, Response[]? responses = null, float responseDelay = 1f, bool network = true)
        {
            S1NPC.SendTextMessage(message);
            S1NPC.MSGConversation.ClearResponses();

            if (responses == null || responses.Length == 0)
            {
	            return;
            }

            Responses.Clear();

            List<S1Messaging.Response> responsesList = new List<S1Messaging.Response>();

            foreach (Response response in responses)
            {
                Responses.Add(response);
                responsesList.Add(response.S1Response);
            }

            S1NPC.MSGConversation.ShowResponses(
                responsesList,
                responseDelay,
                network
            );
        }

        /// <summary>
        /// Gets the instance of an NPC.
        /// Supports base NPCs as well as other mod NPCs.
        /// For base NPCs, <see cref="NPCs"/>.
        /// </summary>
        /// <typeparam name="T">The NPC class to get the instance of.</typeparam>
        /// <returns></returns>
        public static NPC? Get<T>() =>
            All.FirstOrDefault(npc => npc.GetType() == typeof(T));

        #endregion

        // Internal members used by S1API.
        // Please do not attempt to use these members!
        #region Internal Members

        /// <summary>
        /// INTERNAL: Reference to the NPC on the S1 side.
        /// </summary>
        internal readonly S1NPCs.NPC S1NPC;

        /// <summary>
        /// INTERNAL: Constructor used for base game NPCs.
        /// </summary>
        /// <param name="npc">Reference to a base game NPC.</param>
        internal NPC(S1NPCs.NPC npc)
        {
            S1NPC = npc;
            gameObject = npc.gameObject;
            IsCustomNPC = false;
            All.Add(this);
        }

        /// <summary>
        /// INTERNAL: Initializes the responses that have been added / loaded
        /// </summary>
        internal override void CreateInternal()
        {
            // Assign responses to our tracked responses
            foreach (S1Messaging.Response s1Response in S1NPC.MSGConversation.currentResponses)
            {
                Response response = new Response(s1Response) { Label = s1Response.label, Text = s1Response.text };
                Responses.Add(response);
                OnResponseLoaded(response);
            }

            base.CreateInternal();
        }

        internal override void SaveInternal(string folderPath, ref List<string> extraSaveables)
        {
            string npcPath = Path.Combine(folderPath, S1NPC.SaveFolderName);
            base.SaveInternal(npcPath, ref extraSaveables);
        }
        #endregion

        // Private members used by the NPC class.
        // Please do not attempt to use these members!
        #region Private Members

        internal readonly bool IsCustomNPC;

        private readonly FieldInfo _panicField = AccessTools.Field(typeof(S1NPCs.NPC), "PANIC_DURATION");
        private readonly FieldInfo _requiresRegionUnlockedField = AccessTools.Field(typeof(S1NPCs.NPC), "RequiresRegionUnlocked");

        private readonly MethodInfo _unsettleMethod = AccessTools.Method(typeof(S1NPCs.NPC), "SetUnsettled");
        private readonly MethodInfo _removePanicMethod = AccessTools.Method(typeof(S1NPCs.NPC), "RemovePanicked");

        #endregion
    }
}

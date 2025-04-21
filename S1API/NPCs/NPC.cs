#if (IL2CPP)
using S1DevUtilities = Il2CppScheduleOne.DevUtilities;
using S1Interaction = Il2CppScheduleOne.Interaction;
using S1Messaging = Il2CppScheduleOne.Messaging;
using S1Noise = Il2CppScheduleOne.Noise;
using S1Relation = Il2CppScheduleOne.NPCs.Relation;
using S1Responses = Il2CppScheduleOne.NPCs.Responses;
using S1PlayerScripts = Il2CppScheduleOne.PlayerScripts;
using S1ContactApps = Il2CppScheduleOne.UI.Phone.ContactsApp;
using S1WorkspacePopup = Il2CppScheduleOne.UI.WorldspacePopup;
using S1Variables = Il2CppScheduleOne.Variables;
using S1Vehicles = Il2CppScheduleOne.Vehicles;
using S1Vision = Il2CppScheduleOne.Vision;
using S1NPCs = Il2CppScheduleOne.NPCs;
using Il2CppSystem.Collections.Generic;
#elif (MONO)
using S1DevUtilities = ScheduleOne.DevUtilities;
using S1Interaction = ScheduleOne.Interaction;
using S1Messaging = ScheduleOne.Messaging;
using S1Noise = ScheduleOne.Noise;
using S1Relation = ScheduleOne.NPCs.Relation;
using S1Responses = ScheduleOne.NPCs.Responses;
using S1PlayerScripts = ScheduleOne.PlayerScripts;
using S1ContactApps = ScheduleOne.UI.Phone.ContactsApp;
using S1WorkspacePopup = ScheduleOne.UI.WorldspacePopup;
using S1Variables = ScheduleOne.Variables;
using S1Vehicles = ScheduleOne.Vehicles;
using S1Vision = ScheduleOne.Vision;
using S1NPCs = ScheduleOne.NPCs;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
#endif

using System;
using System.IO;
using S1API.Internal.Abstraction;
using UnityEngine;
using UnityEngine.Events;

namespace S1API.NPCs
{
    /// <summary>
    /// An abstract class intended to be derived from for creating custom NPCs in the game.
    /// </summary>
    public abstract class NPC : Saveable
    {
        /// <summary>
        /// A list of text responses you've added to your NPC.
        /// </summary>
        protected readonly System.Collections.Generic.List<Response> Responses = 
            new System.Collections.Generic.List<Response>();
        
        internal readonly S1NPCs.NPC S1NPC;

        private readonly GameObject _gameObject;
        
        /// <summary>
        /// Base constructor for a new NPC.
        /// Intended to be wrapped in your derived class constructor such as:
        /// public class MyNPC : NPC ...
        /// public MyNPC() : base(id, fname, lname) { ... }
        /// </summary>
        /// <param name="guid">The unique identifier for this NPC</param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public NPC(string guid, string firstName, string lastName)
        {
            _gameObject = new GameObject("NPC");
            
            // Deactivate game object til we're done
            _gameObject.SetActive(false);
            
            // Setup the base NPC class
            S1NPC = _gameObject.AddComponent<S1NPCs.NPC>();
            S1NPC.FirstName = firstName;
            S1NPC.LastName = lastName;
            S1NPC.ID = guid;
            S1NPC.BakedGUID = Guid.NewGuid().ToString();
            S1NPC.MugshotSprite = S1DevUtilities.PlayerSingleton<S1ContactApps.ContactsApp>.Instance.AppIcon;

            // ReSharper disable once UseObjectOrCollectionInitializer
            S1NPC.ConversationCategories = new List<S1Messaging.EConversationCategory>();
            S1NPC.ConversationCategories.Add(S1Messaging.EConversationCategory.Customer);
            
            // Create our MessageConversation
#if (IL2CPP)
            S1NPC.CreateMessageConversation();
#elif (MONO)
            MethodInfo createConvoMethod = AccessTools.Method(typeof(S1NPCs.NPC), "CreateMessageConversation");
            createConvoMethod.Invoke(S1NPC, null);
#endif 
            
            // Add UnityEvents for NPCHealth
            S1NPC.Health = _gameObject.GetComponent<S1NPCs.NPCHealth>();
            S1NPC.Health.onDie = new UnityEvent();
            S1NPC.Health.onKnockedOut = new UnityEvent();
            S1NPC.Health.Invincible = true;
            S1NPC.Health.MaxHealth = 100f;
            
            // Awareness behaviour
            GameObject awarenessObject = new GameObject("NPCAwareness");
            awarenessObject.SetActive(false);
            awarenessObject.transform.SetParent(_gameObject.transform);
            S1NPC.awareness = awarenessObject.AddComponent<S1NPCs.NPCAwareness>();
            S1NPC.awareness.onExplosionHeard = new UnityEvent<S1Noise.NoiseEvent>();
            S1NPC.awareness.onGunshotHeard = new UnityEvent<S1Noise.NoiseEvent>();
            S1NPC.awareness.onHitByCar = new UnityEvent<S1Vehicles.LandVehicle>();
            S1NPC.awareness.onNoticedDrugDealing = new UnityEvent<S1PlayerScripts.Player>();
            S1NPC.awareness.onNoticedGeneralCrime = new UnityEvent<S1PlayerScripts.Player>();
            S1NPC.awareness.onNoticedPettyCrime = new UnityEvent<S1PlayerScripts.Player>();
            S1NPC.awareness.onNoticedPlayerViolatingCurfew = new UnityEvent<S1PlayerScripts.Player>();
            S1NPC.awareness.onNoticedSuspiciousPlayer = new UnityEvent<S1PlayerScripts.Player>();
            S1NPC.awareness.Listener = _gameObject.AddComponent<S1Noise.Listener>();
            
            // Response to actions like gunshots, drug deals, etc.
            GameObject responsesObject = new GameObject("NPCResponses");
            responsesObject.SetActive(false);
            responsesObject.transform.SetParent(_gameObject.transform);
            S1NPC.awareness.Responses = responsesObject.AddComponent<S1Responses.NPCResponses_Civilian>();
            
            // Vision cone object and behaviour
            GameObject visionObject = new GameObject("VisionCone");
            visionObject.SetActive(false);
            visionObject.transform.SetParent(_gameObject.transform);
            S1Vision.VisionCone visionCone = visionObject.AddComponent<S1Vision.VisionCone>();
            S1NPC.awareness.VisionCone = visionCone;
            
            // Suspicious ? icon in world space
            S1NPC.awareness.VisionCone.QuestionMarkPopup = _gameObject.AddComponent<S1WorkspacePopup.WorldspacePopup>();
            
            // Interaction behaviour
#if (IL2CPP)
            S1NPC.intObj = _gameObject.AddComponent<S1Interaction.InteractableObject>();
#elif (MONO)
            FieldInfo intObjField = AccessTools.Field(typeof(S1NPCs.NPC), "intObj");
            intObjField.SetValue(S1NPC, _gameObject.AddComponent<S1Interaction.InteractableObject>());
#endif
            
            // Relationship data
            S1NPC.RelationData = new S1Relation.NPCRelationData();

            void OnUnlockAction(S1Relation.NPCRelationData.EUnlockType unlockType, bool notify)
            {
                if (!string.IsNullOrEmpty(S1NPC.NPCUnlockedVariable))
                {
                    S1DevUtilities.NetworkSingleton<S1Variables.VariableDatabase>.Instance.SetVariableValue(S1NPC.NPCUnlockedVariable, true.ToString());
                }
            }

            S1NPC.RelationData.onUnlocked += (Action<S1Relation.NPCRelationData.EUnlockType, bool>)OnUnlockAction;

            // Inventory behaviour
            S1NPCs.NPCInventory inventory = _gameObject.AddComponent<S1NPCs.NPCInventory>();
            
            // Pickpocket behaviour
            inventory.PickpocketIntObj = _gameObject.AddComponent<S1Interaction.InteractableObject>();
            
            // Defaulting to the local player for Avatar TODO: Change
            S1NPC.Avatar = S1PlayerScripts.Player.Local.Avatar;
            
            // Register NPC in registry
            S1NPCs.NPCManager.NPCRegistry.Add(S1NPC);
            
            
            // NetworkObject networkObject = gameObject.AddComponent<NetworkObject>();
            // // networkObject.NetworkBehaviours = InstanceFinder.NetworkManager;
            // PropertyInfo networkBehavioursProperty = AccessTools.Property(typeof(NetworkObject), "NetworkBehaviours");
            // networkBehavioursProperty.SetValue(networkObject, new [] { this });
            
            // Enable our custom gameObjects so they can initialize
            _gameObject.SetActive(true);
            visionObject.SetActive(true);
            responsesObject.SetActive(true);
            awarenessObject.SetActive(true);
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
                return;

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
        /// Called when a response is loaded from the save file.
        /// Override this method for attaching your callbacks to your methods.
        /// </summary>
        /// <param name="response">The response that was loaded.</param>
        protected virtual void OnResponseLoaded(Response response) { }
    }
}
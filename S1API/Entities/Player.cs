#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1PlayerScripts = Il2CppScheduleOne.PlayerScripts;
using S1Health = Il2CppScheduleOne.PlayerScripts.Health;
#else
using S1PlayerScripts = ScheduleOne.PlayerScripts;
using S1Health = ScheduleOne.PlayerScripts.Health;
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using S1API.Entities.Interfaces;
using S1API.Internal.Abstraction;
using UnityEngine;

namespace S1API.Entities
{
    /// <summary>
    /// Represents a player within the game.
    /// </summary>
    public class Player : IEntity, IHealth
    {
        /// <summary>
        /// Health when the player is invincible.
        /// Invincibility isn't baked in the base game.
        /// Hence, why we're doing it this way :).
        /// </summary>
        private const float InvincibleHealth = 1000000000f;

        /// <summary>
        /// The standard MAX_HEALTH of a player.
        /// </summary>
        private const float MortalHealth = 100f;

        /// <summary>
        /// All players currently in the game.
        /// </summary>
        public static readonly List<Player> All = new List<Player>();

        /// <summary>
        /// INTERNAL: Tracking of the S1 instance of the player.
        /// </summary>
        internal S1PlayerScripts.Player S1Player;

        /// <summary>
        /// INTERNAL: Constructor to create a new player from an S1 instance.
        /// </summary>
        /// <param name="player"></param>
        internal Player(S1PlayerScripts.Player player)
        {
            S1Player = player;
            All.Add(this);
        }

        /// <summary>
        /// The current client player (player executing your code).
        /// </summary>
        public static Player Local =>
            All.FirstOrDefault(player => player.IsLocal)!;

        /// <summary>
        /// Whether this player is the client player or a networked player.
        /// </summary>
        public bool IsLocal =>
            S1Player.IsLocalPlayer;

        /// <summary>
        /// The name of the player.
        /// For single player, this appears to always return `Player`.
        /// </summary>
        public string Name =>
            S1Player.PlayerName;

        /// <summary>
        /// INTERNAL: The game object associated with this player.
        /// </summary>
        GameObject IEntity.gameObject =>
            S1Player.gameObject;

        /// <summary>
        /// The world position of the player.
        /// </summary>
        public Vector3 Position
        {
            get => ((IEntity)this).gameObject.transform.position;
            set => ((IEntity)this).gameObject.transform.position = value;
        }

        /// <summary>
        /// The transform of the player.
        /// Please do not set the properties of the Transform.
        /// </summary>
        public Transform Transform =>
            ((IEntity)this).gameObject.transform;

        /// <summary>
        /// The scale of the player.
        /// </summary>
        public float Scale
        {
            get => S1Player.Scale;
            set => S1Player.SetScale(value);
        }

        /// <summary>
        /// The current health of the player.
        /// </summary>
        public float CurrentHealth =>
            S1Player.Health.CurrentHealth;

        /// <summary>
        /// The maximum health of the player.
        /// </summary>
        public float MaxHealth
        {
            get => (float)_maxHealthField.GetValue(S1Player.Health)!;
            set => _maxHealthField.SetValue(S1Player.Health, value);
        }

        /// <summary>
        /// Whether the player is dead or not.
        /// </summary>
        public bool IsDead =>
            !S1Player.Health.IsAlive;

        /// <summary>
        /// Whether the player is invincible or not.
        /// </summary>
        public bool IsInvincible
        {
            get => MaxHealth == InvincibleHealth;
            set
            {
                MaxHealth = value ? InvincibleHealth : MortalHealth;
                S1Player.Health.SetHealth(MaxHealth);
            }
        }

        /// <summary>
        /// Revives the player.
        /// </summary>
        public void Revive() =>
            S1Player.Health.Revive(Position, Quaternion.identity);

        /// <summary>
        /// Deals damage to the player.
        /// </summary>
        /// <param name="amount">The amount of damage to deal.</param>
        public void Damage(int amount)
        {
            if (amount <= 0)
                return;

            S1Player.Health.TakeDamage(amount);
        }

        /// <summary>
        /// Heals the player.
        /// </summary>
        /// <param name="amount">The amount of healing to apply to the player.</param>
        public void Heal(int amount)
        {
            if (amount <= 0)
                return;

            S1Player.Health.SetHealth(CurrentHealth + amount);
        }

        /// <summary>
        /// Kills the player.
        /// </summary>
        public void Kill() =>
            S1Player.Health.SetHealth(0f);

        /// <summary>
        /// Called when the player dies.
        /// </summary>
        public event Action OnDeath
        {
            add => EventHelper.AddListener(value, S1Player.Health.onDie);
            remove => EventHelper.RemoveListener(value, S1Player.Health.onDie);
        }

        /// <summary>
        /// INTERNAL: Field access for the MAX_HEALTH const.
        /// </summary>
        private readonly FieldInfo _maxHealthField = AccessTools.Field(typeof(S1Health.PlayerHealth), "MAX_HEALTH");
    }
}

using System;

namespace S1API.Entities.Interfaces
{
    /// <summary>
    /// Represents an entity that has health associated.
    /// </summary>
    public interface IHealth
    {
        /// <summary>
        /// The current health of the entity.
        /// </summary>
        public float CurrentHealth { get; }
        
        /// <summary>
        /// The max health of the entity.
        /// </summary>
        public float MaxHealth { get; set; }
        
        /// <summary>
        /// Whether the entity is dead or not.
        /// </summary>
        public bool IsDead { get; }
        
        /// <summary>
        /// Whether the entity is invincible.
        /// </summary>
        public bool IsInvincible { get; set; }

        /// <summary>
        /// Revives the entity.
        /// </summary>
        public void Revive();

        /// <summary>
        /// Deals damage to the entity.
        /// </summary>
        /// <param name="amount">Amount of damage to deal.</param>
        public void Damage(int amount);

        /// <summary>
        /// Heals the entity.
        /// </summary>
        /// <param name="amount">Amount of health to heal.</param>
        public void Heal(int amount);

        /// <summary>
        /// Kills the entity.
        /// </summary>
        public void Kill();

        /// <summary>
        /// Called when entity's health is less than or equal to 0.
        /// </summary>
        public event Action OnDeath;
    }
}
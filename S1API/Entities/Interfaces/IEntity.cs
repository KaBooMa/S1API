using UnityEngine;

namespace S1API.Entities.Interfaces
{
    /// <summary>
    /// Represents an entity within the game world.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// INTERNAL: Tracking of the GameObject associated with this entity.
        /// </summary>
        GameObject gameObject { get; }
        
        /// <summary>
        /// The world position of the entity.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// The scale of the entity.
        /// </summary>
        public float Scale { get; set; }
    }
}
#if (IL2CPPMELON)
using S1Economy = Il2CppScheduleOne.Economy;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1Economy = ScheduleOne.Economy;
#endif

using System.Linq;
using S1API.Internal.Abstraction;
using S1API.Storages;
using UnityEngine;

namespace S1API.DeadDrops
{
    /// <summary>
    /// Represents a dead drop in the scene.
    /// </summary>
    public class DeadDropInstance : IGUIDReference
    {
        /// <summary>
        /// INTERNAL: Stores a reference to the game dead drop instance.
        /// </summary>
        internal readonly S1Economy.DeadDrop S1DeadDrop;

        /// <summary>
        /// The cached storage instance.
        /// </summary>
        private StorageInstance? _cachedStorage;

        /// <summary>
        /// INTERNAL: Instances a new dead drop from the game dead drop instance.
        /// </summary>
        /// <param name="deadDrop">The game dead drop instance.</param>
        internal DeadDropInstance(S1Economy.DeadDrop deadDrop) =>
            S1DeadDrop = deadDrop;

        /// <summary>
        /// INTERNAL: Gets a dead drop from a GUID value.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        internal static DeadDropInstance? GetFromGUID(string guid) =>
            DeadDropManager.All.FirstOrDefault(deadDrop => deadDrop.GUID == guid);

        /// <summary>
        /// The unique identifier assigned for this dead drop.
        /// </summary>
        public string GUID =>
            S1DeadDrop.GUID.ToString();

        /// <summary>
        /// The storage container associated with this dead drop.
        /// </summary>
        public StorageInstance Storage =>
            _cachedStorage ??= new StorageInstance(S1DeadDrop.Storage);

        /// <summary>
        /// The world position of the dead drop.
        /// </summary>
        public Vector3 Position =>
            S1DeadDrop.transform.position;
    }
}

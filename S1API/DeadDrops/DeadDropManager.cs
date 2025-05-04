#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1Economy = Il2CppScheduleOne.Economy;
#elif (MONOMELON || MONOBEPINEX)
using S1Economy = ScheduleOne.Economy;
#endif

using System.Linq;

namespace S1API.DeadDrops
{
    /// <summary>
    /// Provides access to managing dead drops across the scene.
    /// </summary>
#pragma warning disable S1118
    public class DeadDropManager
#pragma warning restore S1118
    {
        /// <summary>
        /// Gets all dead drops in the scene.
        /// </summary>
        public static DeadDropInstance[] All =>
            S1Economy.DeadDrop.DeadDrops.ToArray()
                .Select(deadDrop => new DeadDropInstance(deadDrop)).ToArray(); }
}

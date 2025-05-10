#if (IL2CPPMELON)
using S1Economy = Il2CppScheduleOne.Economy;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1Economy = ScheduleOne.Economy;
#endif

using System.Linq;

namespace S1API.DeadDrops
{
    /// <summary>
    /// Provides access to managing dead drops across the scene.
    /// </summary>
    public class DeadDropManager
    {
        /// <summary>
        /// Gets all dead drops in the scene.
        /// </summary>
        public static DeadDropInstance[] All =>
            S1Economy.DeadDrop.DeadDrops.ToArray()
                .Select(deadDrop => new DeadDropInstance(deadDrop)).ToArray(); }
}

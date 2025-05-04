#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1Levelling = Il2CppScheduleOne.Levelling;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1Levelling = ScheduleOne.Levelling;
#endif

namespace S1API.Leveling
{
    /// <summary>
    /// Allows management of the level system.
    /// </summary>
    public static class LevelManager
    {
        /// <summary>
        /// The current rank of the save file.
        /// </summary>
#pragma warning disable S1104
#pragma warning disable S2223
        public static Rank Rank = (Rank)S1Levelling.LevelManager.Instance.Rank;
#pragma warning restore S2223
#pragma warning restore S1104
    }
}

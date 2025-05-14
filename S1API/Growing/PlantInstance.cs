#if (IL2CPPMELON)
using S1Growing = Il2CppScheduleOne.Growing;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1Growing = ScheduleOne.Growing;
#endif

using S1API.Internal.Utils;
using S1API.Items;
using UnityEngine;

namespace S1API.Growing
{
    /// <summary>
    /// Represents an instance of a growing plant in the world.
    /// </summary>
    public class PlantInstance
    {
        /// <summary>
        /// INTERNAL: The in-game Plant object.
        /// </summary>
        internal readonly S1Growing.Plant S1Plant;

        /// <summary>
        /// INTERNAL: Create a wrapper around an existing Plant.
        /// </summary>
        /// <param name="plant">The in-game Plant to wrap.</param>
        internal PlantInstance(S1Growing.Plant plant)
        {
            S1Plant = plant;
        }

        /// <summary>
        /// The current growth stage as a float from 0.0 to 1.0.
        /// </summary>
        public float NormalizedGrowth =>
            S1Plant.NormalizedGrowthProgress;

        /// <summary>
        /// Whether the plant is fully grown.
        /// </summary>
        public bool IsFullyGrown =>
            S1Plant.IsFullyGrown;

        /// <summary>
        /// The SeedDefinition that this plant originated from.
        /// </summary>
        public SeedDefinition SeedDefinition =>
            new SeedDefinition(S1Plant.SeedDefinition);

        /// <summary>
        /// The quality level of this plant.
        /// </summary>
        public float Quality =>
            S1Plant.QualityLevel;

        /// <summary>
        /// The yield level (amount) of this plant.
        /// </summary>
        public float Yield =>
            S1Plant.YieldLevel;

        /// <summary>
        /// The GameObject of the plant.
        /// </summary>
        private GameObject GameObject =>
            S1Plant.gameObject;

        /// <summary>
        /// Destroys this plant in-game.
        /// </summary>
        /// <param name="dropScraps">Whether to drop trash scraps.</param>
        public void Destroy(bool dropScraps = false)
        {
            S1Plant.Destroy(dropScraps);
        }
    }
}

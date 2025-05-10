#if (IL2CPPMELON)
using S1Growing = Il2CppScheduleOne.Growing;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1Growing = ScheduleOne.Growing;
#endif

using System;
using UnityEngine;

using S1API.Internal.Utils;
using S1API.Items;

namespace S1API.Growing
{
    /// <summary>
    /// Represents the definition of a Seed item (what you buy in shops).
    /// </summary>
    public class SeedDefinition : ItemDefinition
    {
        /// <summary>
        /// INTERNAL: Stored reference to the SeedDefinition.
        /// </summary>
        internal S1Growing.SeedDefinition S1SeedDefinition =>
            CrossType.As<S1Growing.SeedDefinition>(S1ItemDefinition);

        /// <summary>
        /// INTERNAL: Create a new wrapper around an existing SeedDefinition.
        /// </summary>
        /// <param name="definition">The in-game SeedDefinition to wrap.</param>
        internal SeedDefinition(S1Growing.SeedDefinition definition) : base(definition) { }

        /// <summary>
        /// The prefab that is spawned when planting this seed.
        /// </summary>
        public GameObject? FunctionalSeedPrefab => S1SeedDefinition.FunctionSeedPrefab?.gameObject;

        /// <summary>
        /// The plant prefab this seed grows into.
        /// </summary>
        public GameObject? PlantPrefab => S1SeedDefinition.PlantPrefab?.gameObject;

        /// <summary>
        /// Creates an instance of this seed in the world (FunctionalSeed prefab).
        /// </summary>
        public GameObject CreateSeedInstance()
        {
            if (S1SeedDefinition.FunctionSeedPrefab != null)
                return UnityEngine.Object.Instantiate(S1SeedDefinition.FunctionSeedPrefab).gameObject;

            throw new NullReferenceException("No FunctionalSeedPrefab assigned to this SeedDefinition!");
        }


    }
}

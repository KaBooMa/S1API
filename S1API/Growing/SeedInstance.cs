#if (IL2CPPMELON)
using S1Growing = Il2CppScheduleOne.Growing;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1Growing = ScheduleOne.Growing;
#endif

using UnityEngine;
using S1API.Internal.Utils;

namespace S1API.Growing
{
    /// <summary>
    /// Represents an instance of a functional seed in the world.
    /// (Not just the definition — this is the physical object you interact with.)
    /// </summary>
    public class SeedInstance
    {
        /// <summary>
        /// INTERNAL: Reference to the in-game FunctionalSeed object.
        /// </summary>
        internal readonly S1Growing.FunctionalSeed S1FunctionalSeed;

        /// <summary>
        /// INTERNAL: Creates a wrapper around the existing FunctionalSeed.
        /// </summary>
        /// <param name="functionalSeed">The FunctionalSeed object to wrap.</param>
        internal SeedInstance(S1Growing.FunctionalSeed functionalSeed)
        {
            S1FunctionalSeed = functionalSeed;
        }

        /// <summary>
        /// The underlying GameObject of this seed.
        /// </summary>
        private GameObject GameObject =>
            S1FunctionalSeed.gameObject;

        /// <summary>
        /// Whether the seed currently has exited its vial.
        /// </summary>
        public bool HasExitedVial { get; private set; } = false;

        /// <summary>
        /// Force the seed to exit the vial manually.
        /// </summary>
        public void ForceExitVial()
        {
            if (S1FunctionalSeed.Vial != null)
            {
                S1FunctionalSeed.TriggerExit(S1FunctionalSeed.Vial.GetComponent<Collider>());
                HasExitedVial = true;
            }
        }
    }
}

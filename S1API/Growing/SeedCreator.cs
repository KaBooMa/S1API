#if (IL2CPPMELON)
using S1Growing = Il2CppScheduleOne.Growing;
using S1ItemFramework = Il2CppScheduleOne.ItemFramework;
using S1Registry = Il2CppScheduleOne.Registry;
#elif  (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1Growing = ScheduleOne.Growing;
using S1ItemFramework = ScheduleOne.ItemFramework;
using S1Registry = ScheduleOne.Registry;
#endif

using UnityEngine;

namespace S1API.Growing
{
    /// <summary>
    /// The seed Creator for custom seeds to be added.
    /// </summary>
    public static class SeedCreator
    {
        public static SeedDefinition CreateSeed(
            string id,
            string name,
            string description,
            int stackLimit = 10,
            GameObject? functionSeedPrefab = null,
            GameObject? plantPrefab = null,
            Sprite? icon = null)
        {
            S1Growing.SeedDefinition seed = ScriptableObject.CreateInstance<S1Growing.SeedDefinition>();

            seed.ID = id;
            seed.Name = name;
            seed.Description = description;
            seed.StackLimit = stackLimit;
            seed.Category = S1ItemFramework.EItemCategory.Growing;

           // if (icon != null)
           // {
           //     seed.Icon = icon;
           // }
           // commented out for more test later.

            if (functionSeedPrefab != null)
                seed.FunctionSeedPrefab = functionSeedPrefab.GetComponent<S1Growing.FunctionalSeed>();

            if (plantPrefab != null)
                seed.PlantPrefab = plantPrefab.GetComponent<S1Growing.Plant>();

            S1Registry.Instance.AddToRegistry(seed);

            return new SeedDefinition(seed);
        }

    }
}

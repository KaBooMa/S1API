#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs
{
    /// <summary>
    /// Stan Carney is a NPC.
    /// He is the NPC that sells weapons.
    /// Stan can be found in the Warehouse!
    /// </summary>
    public class StanCarney : NPC
    {
        internal StanCarney() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "stan_carney")) { }
    }
}

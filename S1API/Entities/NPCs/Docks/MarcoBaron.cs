#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Docks
{
    /// <summary>
    /// Marco Baron is a customer.
    /// He lives in the Docks region.
    /// Marco is the NPC that runs the Auto Shop!
    /// </summary>
    public class MarcoBaron : NPC
    {
        internal MarcoBaron() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "marco_baron")) { }
    }
}

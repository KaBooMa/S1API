#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;
using NPC = S1API.Entities.NPC;

namespace S1API.Entities.NPCs.Uptown
{
    /// <summary>
    /// Pearl Moore is a customer.
    /// She lives in the Uptown region.
    /// Pearl is the NPC with long white hair with bangs!
    /// </summary>
    public class PearlMoore : NPC
    {
        internal PearlMoore() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "pearl_moore")) { }
    }
}

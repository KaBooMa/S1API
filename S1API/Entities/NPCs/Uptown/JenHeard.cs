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
    /// Jen Heard is a customer.
    /// She lives in the Uptown region.
    /// Jen is the NPC with low orange buns!
    /// </summary>
    public class JenHeard : NPC
    {
        internal JenHeard() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "jen_heard")) { }
    }
}

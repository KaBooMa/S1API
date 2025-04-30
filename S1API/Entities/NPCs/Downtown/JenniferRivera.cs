#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Downtown
{
    /// <summary>
    /// Jennifer Rivera is a customer.
    /// She lives in the Downtown region.
    /// Jennifer is the NPC with blonde haired buns!
    /// </summary>
    public class JenniferRivera : NPC
    {
        internal JenniferRivera() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "jennifer_rivera")) { }
    }
}

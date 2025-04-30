#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Westville
{
    /// <summary>
    /// Jerry Montero is a customer.
    /// He lives in the Westville region.
    /// Jerry is the NPC with a green hat and black glasses!
    /// </summary>
    public class JerryMontero : NPC
    {
        internal JerryMontero() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "jerry_montero")) { }
    }
}

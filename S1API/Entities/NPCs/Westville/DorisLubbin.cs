#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Westville
{
    /// <summary>
    /// Doris Lubbin is a customer.
    /// She lives in the Westville region.
    /// Doris is the NPC with light brown, wavy hair and black glasses!
    /// </summary>
    public class DorisLubbin : NPC
    {
        internal DorisLubbin() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "doris_lubbin")) { }
    }
}

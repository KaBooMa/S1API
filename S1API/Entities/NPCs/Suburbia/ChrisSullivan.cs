#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Suburbia
{
    /// <summary>
    /// Chris Sullivan is a customer.
    /// He lives in the Suburbia region.
    /// Chris is the NPC with black spiky hair and black glasses!
    /// </summary>
    public class ChrisSullivan : NPC
    {
        internal ChrisSullivan() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "chris_sullivan")) { }
    }
}

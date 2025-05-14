#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Suburbia
{
    /// <summary>
    /// Carl Bundy is a customer.
    /// He lives in the Suburbia region.
    /// Carl is the NPC with a brown apron!
    /// </summary>
    public class CarlBundy : NPC
    {
        internal CarlBundy() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "carl_bundy")) { }
    }
}

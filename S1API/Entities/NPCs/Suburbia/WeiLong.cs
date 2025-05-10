#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Suburbia
{
    /// <summary>
    /// Wei Long is a dealer.
    /// He lives in the Suburbia region.
    /// Wei is the dealer with a black bowl cut and gold glasses!
    /// </summary>
    public class WeiLong : NPC
    {
        internal WeiLong() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "wei_long")) { }
    }
}

#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Northtown
{
    /// <summary>
    /// Mick Lubbin is a customer.
    /// He lives in the Northtown region.
    /// Mick is the owner of the pawn shop!
    /// </summary>
    public class MickLubbin : NPC
    {
        internal MickLubbin() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "mick_lubbin")) { }
    }
}

#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Docks
{
    /// <summary>
    /// Mac Cooper is a customer.
    /// He lives in the Docks region.
    /// Mac is the NPC with a blonde mohawk and gold shades!
    /// </summary>
    public class MacCooper : NPC
    {
        internal MacCooper() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "mac_cooper")) { }
    }
}

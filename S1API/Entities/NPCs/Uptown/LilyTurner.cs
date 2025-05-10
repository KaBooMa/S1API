#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;
using NPC = S1API.Entities.NPC;

namespace S1API.Entities.NPCs.Uptown
{
    /// <summary>
    /// Lily Turner is a customer.
    /// She lives in the Uptown region.
    /// Lily is the NPC with long brown hair with bangs!
    /// </summary>
    public class LilyTurner : NPC
    {
        internal LilyTurner() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "lily_turner")) { }
    }
}

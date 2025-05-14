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
    /// Leo Rivers is a dealer.
    /// He lives in the Uptown region.
    /// Leo is the dealer wearing a black hat and gold shades!
    /// </summary>
    public class LeoRivers : NPC
    {
        internal LeoRivers() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "leo_rivers")) { }
    }
}

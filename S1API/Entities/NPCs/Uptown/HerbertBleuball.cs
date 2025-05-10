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
    /// Herbert Bleuball is a customer.
    /// He lives in the Uptown region.
    /// Herbert is the NPC that owns Bleuball's Boutique!
    /// </summary>
    public class HerbertBleuball : NPC
    {
        internal HerbertBleuball() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "herbert_bleuball")) { }
    }
}

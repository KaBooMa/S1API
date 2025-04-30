#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Downtown
{
    /// <summary>
    /// Philip Wentworth is a customer.
    /// He lives in the Downtown region.
    /// Philip is the bald NPC with a goatee!
    /// </summary>
    public class PhilipWentworth : NPC
    {
        internal PhilipWentworth() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "philip_wentworth")) { }
    }
}

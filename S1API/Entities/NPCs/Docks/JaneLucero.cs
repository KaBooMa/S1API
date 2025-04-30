#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Docks
{
    /// <summary>
    /// Jane Lucero is a dealer.
    /// She lives in the Docks region.
    /// Jane is the dealer with a tear tattoo!
    /// </summary>
    public class JaneLucero : NPC
    {
        internal JaneLucero() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "jane_lucero")) { }
    }
}

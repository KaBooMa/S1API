#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Northtown
{
    /// <summary>
    /// Sam Thompson is a customer.
    /// He lives in the Northtown region.
    /// Sam is the NPC with a green hair and wrinkles!
    /// </summary>
    public class SamThompson : NPC
    {
        internal SamThompson() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "sam_thompson")) { }
    }
}

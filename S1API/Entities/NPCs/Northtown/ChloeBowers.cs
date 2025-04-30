#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Northtown
{
    /// <summary>
    /// Chloe Bowers is a customer.
    /// She lives in the Northtown region.
    /// Chloe is the NPC with long, straight, red hair!
    /// </summary>
    public class ChloeBowers : NPC
    {
        internal ChloeBowers() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "chloe_bowers")) { }
    }
}

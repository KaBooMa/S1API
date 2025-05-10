#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Northtown
{
    /// <summary>
    /// Peggy Myers is a customer.
    /// She lives in the Northtown region.
    /// Peggy is the NPC with freckles and brown hair pulled back!
    /// </summary>
    public class PeggyMyers : NPC
    {
        internal PeggyMyers() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "peggy_myers")) { }
    }
}

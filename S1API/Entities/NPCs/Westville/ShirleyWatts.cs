#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Westville
{
    /// <summary>
    /// Shirley Watts is a supplier.
    /// She lives in the Westville region.
    /// Shirley is the supplier for pseudo!
    /// </summary>
    public class ShirleyWatts : NPC
    {
        internal ShirleyWatts() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "shirley_watts")) { }
    }
}

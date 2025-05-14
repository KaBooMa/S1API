#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Northtown
{
    /// <summary>
    /// Donna Martin is a customer.
    /// She lives in the Northtown region.
    /// Donna is the attendant of the Motel!
    /// </summary>
    public class DonnaMartin : NPC
    {
        internal DonnaMartin() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "donna_martin")) { }
    }
}

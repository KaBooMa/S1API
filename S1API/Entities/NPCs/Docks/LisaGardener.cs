#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Docks
{
    /// <summary>
    /// Lisa Gardener is a customer.
    /// She lives in the Docks region.
    /// Lisa is the NPC wearing blue scrubs!
    /// </summary>
    public class LisaGardener : NPC
    {
        internal LisaGardener() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "lisa_gardener")) { }
    }
}

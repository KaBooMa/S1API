#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Northtown
{
    /// <summary>
    /// Mrs. Ming is a customer.
    /// She lives in the Northtown region.
    /// Ming is the NPC that owns the chinese restaurant!
    /// </summary>
    public class Ming : NPC
    {
        internal Ming() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "ming")) { }
    }
}

#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Westville
{
    /// <summary>
    /// Keith Wagner is a customer.
    /// He lives in the Westville region.
    /// Keith is the NPC with blonde spiky hair and always angry!
    /// </summary>
    public class KeithWagner : NPC
    {
        internal KeithWagner() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "keith_wagner")) { }
    }
}

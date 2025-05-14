#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs
{
    /// <summary>
    /// Uncle Nelson is a NPC.
    /// He is the uncle of the main character!
    /// </summary>
    public class UncleNelson : NPC
    {
        internal UncleNelson() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "uncle_nelson")) { }
    }
}

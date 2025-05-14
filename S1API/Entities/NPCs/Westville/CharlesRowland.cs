#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Westville
{
    /// <summary>
    /// Charles Rowland is a customer.
    /// He lives in the Westville region.
    /// Charles is the bald NPC with black glasses!
    /// </summary>
    public class CharlesRowland : NPC
    {
        internal CharlesRowland() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "charles_rowland")) { }
    }
}

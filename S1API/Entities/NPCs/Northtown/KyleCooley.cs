#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Northtown
{
    /// <summary>
    /// Kyle Cooley is a customer.
    /// He lives in the Northtown region.
    /// Kyle is the NPC that works at Taco Ticklers!
    /// </summary>
    public class KyleCooley : NPC
    {
        internal KyleCooley() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "kyle_cooley")) { }
    }
}

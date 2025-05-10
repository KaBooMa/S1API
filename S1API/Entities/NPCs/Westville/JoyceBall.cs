#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Westville
{
    /// <summary>
    /// Joyce Ball is a customer.
    /// She lives in the Westville region.
    /// Joyce is the NPC with light brown hair and wrinkles!
    /// </summary>
    public class JoyceBall : NPC
    {
        internal JoyceBall() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "joyce_ball")) { }
    }
}

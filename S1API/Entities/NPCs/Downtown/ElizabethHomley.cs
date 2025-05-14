#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Downtown
{
    /// <summary>
    /// Elizabeth Homley is a customer.
    /// She lives in the Downtown region.
    /// Elizabeth is the NPC is lightning blue hair!
    /// </summary>
    public class ElizabethHomley : NPC
    {
        internal ElizabethHomley() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "elizabeth_homley")) { }
    }
}

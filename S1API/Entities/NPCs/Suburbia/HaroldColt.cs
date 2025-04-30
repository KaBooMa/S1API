#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Suburbia
{
    /// <summary>
    /// Harold Colt is a customer.
    /// He lives in the Suburbia region.
    /// Harold is the NPC with grey spiky hair and wrinkles!
    /// </summary>
    public class HaroldColt : NPC
    {
        internal HaroldColt() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "harold_colt")) { }
    }
}

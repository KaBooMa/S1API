#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Downtown
{
    /// <summary>
    /// Eugene Buckley is a customer.
    /// He lives in the Downtown region.
    /// Eugene is the NPC with light brown hair, freckles, and black glasses!
    /// </summary>
    public class EugeneBuckley : NPC
    {
        internal EugeneBuckley() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "eugene_buckley")) { }
    }
}

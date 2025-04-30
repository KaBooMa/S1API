#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Suburbia
{
    /// <summary>
    /// Jackie Stevenson is a customer.
    /// He lives in the Suburbia region.
    /// Jackie is the NPC with short brown hair and light freckles!
    /// </summary>
    public class JackieStevenson : NPC
    {
        internal JackieStevenson() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "jackie_stevenson")) { }
    }
}

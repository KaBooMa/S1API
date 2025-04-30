#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Suburbia
{
    /// <summary>
    /// Hank Stevenson is a customer.
    /// He lives in the Suburbia region.
    /// Hank is the balding NPC with greying brown hair and a goatee!
    /// </summary>
    public class HankStevenson : NPC
    {
        internal HankStevenson() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "hank_stevenson")) { }
    }
}

#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Suburbia
{
    /// <summary>
    /// Alison Knight is a customer.
    /// She lives in the Suburbia region.
    /// Alison is the NPC with long light brown hair!
    /// </summary>
    public class AlisonKnight : NPC
    {
        internal AlisonKnight() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "alison_knight")) { }
    }
}

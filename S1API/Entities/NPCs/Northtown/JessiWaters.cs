#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Northtown
{
    /// <summary>
    /// Jessi Waters is a customer.
    /// She lives in the Northtown region.
    /// Jessi is the purple haired NPC with face tattoos!
    /// </summary>
    public class JessiWaters : NPC
    {
        internal JessiWaters() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "jessi_waters")) { }
    }
}

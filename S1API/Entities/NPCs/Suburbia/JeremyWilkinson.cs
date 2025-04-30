#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Suburbia
{
    /// <summary>
    /// Jeremy Wilkinson is a customer.
    /// He lives in the Suburbia region.
    /// Jeremy is the NPC that works at Hyland Auto!
    /// </summary>
    public class JeremyWilkinson : NPC
    {
        internal JeremyWilkinson() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "jeremy_wilkinson")) { }
    }
}

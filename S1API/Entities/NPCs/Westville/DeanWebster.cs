#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Westville
{
    /// <summary>
    /// Dean Webster is a customer.
    /// He lives in the Westville region.
    /// Dean is the NPC that owns Top Tattoo!
    /// </summary>
    public class DeanWebster : NPC
    {
        internal DeanWebster() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "dean_webster")) { }
    }
}

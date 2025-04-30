#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Northtown
{
    /// <summary>
    /// Austin Steiner is a customer.
    /// He lives in the Northtown region.
    /// Austin is the NPC with a red/orange afro and black glasses!
    /// </summary>
    public class AustinSteiner : NPC
    {
        internal AustinSteiner() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "austin_steiner")) { }
    }
}

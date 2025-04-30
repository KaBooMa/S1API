#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Westville
{
    /// <summary>
    /// Meg Cooley is a customer.
    /// She lives in the Westville region.
    /// Meg is the npc with a mustard yellow bowl cut hairstyle!
    /// </summary>
    public class MegCooley : NPC
    {
        internal MegCooley() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "meg_cooley")) { }
    }
}

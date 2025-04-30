#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Downtown
{
    /// <summary>
    /// Lucy Pennington is a customer.
    /// She lives in the Downtown region.
    /// Lucy is the NPC with blonde haired buns up high!
    /// </summary>
    public class LucyPennington : NPC
    {
        internal LucyPennington() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "lucy_pennington")) { }
    }
}

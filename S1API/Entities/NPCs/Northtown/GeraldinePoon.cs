#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Northtown
{
    /// <summary>
    /// Geraldine Poon is a customer.
    /// He lives in the Northtown region.
    /// Geraldine is the balding NPC with small gold glasses!
    /// </summary>
    public class GeraldinePoon : NPC
    {
        internal GeraldinePoon() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "geraldine_poon")) { }
    }
}

#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;
using NPC = S1API.Entities.NPC;

namespace S1API.Entities.NPCs.Uptown
{
    /// <summary>
    /// Tobias Wentworth is a customer.
    /// He lives in the Uptown region.
    /// Tobias is the balding NPC with extremely light brown hair and small black glasses!
    /// </summary>
    public class TobiasWentworth : NPC
    {
        internal TobiasWentworth() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "tobias_wentworth")) { }
    }
}

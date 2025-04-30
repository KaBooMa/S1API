#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs
{
    /// <summary>
    /// Oscar Holland is a NPC.
    /// He is a supplier located in the Warehouse!
    /// </summary>
    public class OscarHolland : NPC
    {
        internal OscarHolland() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "oscar_holland")) { }
    }
}

#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Docks
{
    /// <summary>
    /// Javier Perez is a customer.
    /// He lives in the Docks region.
    /// Javier works night shift at the Gas-Mart!
    /// </summary>
    public class JavierPerez : NPC
    {
        internal JavierPerez() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "javier_perez")) { }
    }
}

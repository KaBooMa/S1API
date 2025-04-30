#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Docks
{
    /// <summary>
    /// Billy Kramer is a customer.
    /// He lives in the Docks region.
    /// </summary>
    public class BillyKramer : NPC
    {
        internal BillyKramer() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "billy_kramer")) { }
    }
}

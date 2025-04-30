#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Docks
{
    /// <summary>
    /// Anna Chesterfield is a customer.
    /// She lives in the Docks region.
    /// Anna also works at the Barbershop.
    /// </summary>
    public class AnnaChesterfield : NPC
    {
        internal AnnaChesterfield() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "anna_chesterfield")) { }
    }
}

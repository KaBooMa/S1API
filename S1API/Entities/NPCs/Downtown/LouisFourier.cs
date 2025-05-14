#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Downtown
{
    /// <summary>
    /// Louis Fourier is a customer.
    /// He lives in the Downtown region.
    /// Louis is the NPC with a chef's hat!
    /// </summary>
    public class LouisFourier : NPC
    {
        internal LouisFourier() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "louis_fourier")) { }
    }
}

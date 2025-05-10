#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Downtown
{
    /// <summary>
    /// Greg Fliggle is a customer.
    /// He lives in the Downtown region.
    /// Greg is the NPC with a teardrop tattoo and wrinkles!
    /// </summary>
    public class GregFliggle : NPC
    {
        internal GregFliggle() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "greg_fliggle")) { }
    }
}

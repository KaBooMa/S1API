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
    /// Walter Cussler is a customer.
    /// He lives in the Uptown region.
    /// Walter is the NPC with white hair and dressed as a priest!
    /// </summary>
    public class WalterCussler : NPC
    {
        internal WalterCussler() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "walter_cussler")) { }
    }
}

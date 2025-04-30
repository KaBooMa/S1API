#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Westville
{
    /// <summary>
    /// Molly Presley is a dealer.
    /// She lives in the Westville region.
    /// Molly is the dealer with gold shades and a red backward cap!
    /// </summary>
    public class MollyPresley : NPC
    {
        internal MollyPresley() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "molly_presley")) { }
    }
}

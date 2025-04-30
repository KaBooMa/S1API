#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Northtown
{
    /// <summary>
    /// Albert Hoover is a supplier.
    /// He lives in the Northtown region.
    /// Albert is the supplier for weed seeds!
    /// </summary>
    public class AlbertHoover : NPC
    {
        internal AlbertHoover() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "albert_hoover")) { }
    }
}

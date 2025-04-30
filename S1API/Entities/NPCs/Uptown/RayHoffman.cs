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
    /// Ray Hoffman is a customer.
    /// He lives in the Uptown region.
    /// Ray is the NPC that owns Ray's Realty!
    /// </summary>
    public class RayHoffman : NPC
    {
        internal RayHoffman() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "ray_hoffman")) { }
    }
}

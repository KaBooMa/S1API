#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Northtown
{
    /// <summary>
    /// Ludwig Meyer is a customer.
    /// He lives in the Northtown region.
    /// Ludwig is the NPC with spiky hair and gold glasses!
    /// </summary>
    public class LudwigMeyer : NPC
    {
        internal LudwigMeyer() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "ludwig_meyer")) { }
    }
}

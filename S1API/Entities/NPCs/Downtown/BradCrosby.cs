#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Downtown
{
    /// <summary>
    /// Brad Crosby is a dealer.
    /// He lives in the Downtown region.
    /// Brad lives in a tent at the parking garage next to the casino!
    /// </summary>
    public class BradCrosby : NPC
    {
        internal BradCrosby() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "brad_crosby")) { }
    }
}

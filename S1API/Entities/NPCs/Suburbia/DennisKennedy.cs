#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Suburbia
{
    /// <summary>
    /// Dennis Kennedy is a customer.
    /// He lives in the Suburbia region.
    /// Dennis is the NPC with light blonde spiky hair and a thick mustache!
    /// </summary>
    public class DennisKennedy : NPC
    {
        internal DennisKennedy() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "dennis_kennedy")) { }
    }
}

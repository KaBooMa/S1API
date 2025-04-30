#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.PoliceOfficers
{
    /// <summary>
    /// Officer Bailey is a police officer.
    /// He is the bald officer with a swirling mustache!
    /// </summary>
    public class OfficerBailey : NPC
    {
        internal OfficerBailey() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "officerbailey")) { }
    }
}

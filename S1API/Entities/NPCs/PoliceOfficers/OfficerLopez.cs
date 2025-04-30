#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.PoliceOfficers
{
    /// <summary>
    /// Officer Lopez is a police officer.
    /// She is the officer with a blue button-up and long black hair!
    /// </summary>
    public class OfficerLopez : NPC
    {
        internal OfficerLopez() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "officerlopez")) { }
    }
}

#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.PoliceOfficers
{
    /// <summary>
    /// Officer Lee is a police officer.
    /// He is the officer with a button-up shirt and black hair!
    /// </summary>
    public class OfficerLee : NPC
    {
        internal OfficerLee() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "officerlee")) { }
    }
}

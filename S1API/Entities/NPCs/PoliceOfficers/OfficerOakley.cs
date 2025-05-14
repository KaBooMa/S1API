#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.PoliceOfficers
{
    /// <summary>
    /// Officer Oakley is a police officer.
    /// He is the officer with light brown spiky hair and a goatee!
    /// </summary>
    public class OfficerOakley : NPC
    {
        internal OfficerOakley() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "officeroakley")) { }
    }
}

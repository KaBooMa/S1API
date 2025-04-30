#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.PoliceOfficers
{
    /// <summary>
    /// Officer Cooper is a police officer.
    /// She is the officer with two high black buns and black glasses!
    /// </summary>
    public class OfficerCooper : NPC
    {
        internal OfficerCooper() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "officercooper")) { }
    }
}

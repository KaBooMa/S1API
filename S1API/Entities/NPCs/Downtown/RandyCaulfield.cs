#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Downtown
{
    /// <summary>
    /// Randy Caulfield is a customer.
    /// He lives in the Downtown region.
    /// Randy is the NPC wearing a green hat!
    /// </summary>
    public class RandyCaulfield : NPC
    {
        internal RandyCaulfield() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "randy_caulfield")) { }
    }
}

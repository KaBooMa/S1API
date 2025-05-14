#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Suburbia
{
    /// <summary>
    /// Karen Kennedy is a customer.
    /// She lives in the Suburbia region.
    /// Karen is the NPC with wavy blonde hair and purple eyelids!
    /// She can be found at the casino upstairs when it's open.
    /// </summary>
    public class KarenKennedy : NPC
    {
        internal KarenKennedy() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "karen_kennedy")) { }
    }
}

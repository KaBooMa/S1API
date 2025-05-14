#if (IL2CPPMELON)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;
using NPC = S1API.Entities.NPC;

namespace S1API.Entities.NPCs.Uptown
{
    /// <summary>
    /// Fiona Hancock is a customer.
    /// She lives in the Uptown region.
    /// Fiona is the NPC with light brown buns and green glasses!
    /// </summary>
    public class FionaHancock : NPC
    {
        internal FionaHancock() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "fiona_hancock")) { }
    }
}

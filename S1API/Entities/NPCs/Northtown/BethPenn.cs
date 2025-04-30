#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Northtown
{
    /// <summary>
    /// Beth Penn is a customer.
    /// She lives in the Northtown region.
    /// Beth is the NPC with a blonde bowl cut and wears green glasses!
    /// </summary>
    public class BethPenn : NPC
    {
        internal BethPenn() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "beth_penn")) { }
    }
}

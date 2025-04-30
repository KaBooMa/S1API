#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;
using NPC = S1API.Entities.NPC;

namespace S1API.Entities.NPCs.Uptown
{
    /// <summary>
    /// Michael Boog is a customer.
    /// He lives in the Uptown region.
    /// Michael is the NPC with a bright blue flat cap and black glasses!
    /// </summary>
    public class MichaelBoog : NPC
    {
        internal MichaelBoog() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "michael_boog")) { }
    }
}

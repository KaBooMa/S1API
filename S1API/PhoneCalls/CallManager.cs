#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1Calling = Il2CppScheduleOne.Calling;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1Calling = ScheduleOne.Calling;
#endif

namespace S1API.PhoneCalls
{
    /// <summary>
    /// @TODO: DOCS
    /// </summary>
    public static class CallManager
    {
        /// <summary>
        /// @TODO: DOCS
        /// </summary>
        /// <param name="phoneCallDefinition"></param>
        public static void QueueCall(PhoneCallDefinition phoneCallDefinition)
        {
            S1Calling.CallManager.Instance.QueueCall(phoneCallDefinition.S1PhoneCallData);
        }
    }
}

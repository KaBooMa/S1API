#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppSystem.Collections.Generic;
using static Il2CppScheduleOne.Console;
#else
using static ScheduleOne.Console;
using System.Collections.Generic;
#endif

namespace S1API.Console
{
    /// <summary>
    /// This class provides easy access to the in-game console system.
    /// </summary>
    public static class ConsoleHelper
    {
        /// <summary>
        /// Executes the ChangeCashCommand with the given amount.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="amount">The cash amount to add or remove.</param>
        public static void RunCashCommand(int amount)
        {
#if (IL2CPPMELON || IL2CPPBEPINEX)
            var command = new ChangeCashCommand();
            var args = new Il2CppSystem.Collections.Generic.List<string>();
#else
            var command = new ChangeCashCommand();
            var args = new List<string>();
#endif
            args.Add(amount.ToString());
            command.Execute(args);
        }
    }
}

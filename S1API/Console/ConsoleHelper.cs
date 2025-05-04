#if (IL2CPPMELON)
using Il2CppSystem.Collections.Generic;
using static Il2CppScheduleOne.Console;
using Il2CppScheduleOne.Employees;
#else
using static ScheduleOne.Console;
using System.Collections.Generic;
using ScheduleOne.Employees;
#endif
using S1API.Property;

namespace S1API.Console
{
    /// <summary>
    /// This class provides easy access to the in-game console system.
    /// </summary>
    public static class ConsoleHelper
    {
        /// <summary>
        /// Executes the AddEmployeeCommand with the given arguments.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="employeeType">The type of employee.</param>
        /// <param name="propertyName">The property to add the employee to. You must own the property.</param>
        public static void AddEmployee(EEmployeeType employeeType, PropertyType propertyName)
        {
#if (IL2CPPMELON || IL2CPPBEPINEX)
            var command = new AddEmployeeCommand();
            var args = new Il2CppSystem.Collections.Generic.List<string>();
#else
            var command = new AddEmployeeCommand();
            var args = new List<string>();
#endif
            args.Add(employeeType.ToString());
            args.Add(propertyName.ToString());
            
            command.Execute(args);
        }
        
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

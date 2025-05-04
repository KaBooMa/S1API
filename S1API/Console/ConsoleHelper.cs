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
        /// Adds an employee to a property.
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
        /// Adds an item, with optional quantity, to the player's inventory.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="itemName">The name of the item.</param>
        /// <param name="amount">The amount to add to inventory. Optional.</param>
        public static void AddItemToInventory(string itemName, int? amount = null)
        {
#if (IL2CPPMELON || IL2CPPBEPINEX)
            var command = new AddItemToInventoryCommand();
            var args = new Il2CppSystem.Collections.Generic.List<string>();
#else
            var command = new AddItemToInventoryCommand();
            var args = new List<string>();
#endif
            args.Add(itemName);
            if (amount.HasValue)
            {
                args.Add(amount.ToString()!);
            }

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets the player's bank balance to the given amount.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="amount">The amount to set the player's bank balance to.</param>
        public static void ChangeOnlineBalance(int amount)
        {
#if (IL2CPPMELON || IL2CPPBEPINEX)
            var command = new ChangeOnlineBalanceCommand();
            var args = new Il2CppSystem.Collections.Generic.List<string>();
#else
            var command = new ChangeOnlineBalanceCommand();
            var args = new List<string>();
#endif
            args.Add(amount.ToString());

            command.Execute(args);
        }
        
        /// <summary>
        /// Clears the player's inventory.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void ClearInventory()
        {
#if (IL2CPPMELON || IL2CPPBEPINEX)
            var command = new ClearInventoryCommand();
            var args = new Il2CppSystem.Collections.Generic.List<string>();
#else
            var command = new ClearInventoryCommand();
            var args = new List<string>();
#endif

            command.Execute(args);
        }
        
        /// <summary>
        /// Clears all trash from the world.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void ClearTrash()
        {
#if (IL2CPPMELON || IL2CPPBEPINEX)
            var command = new ClearTrash();
            var args = new Il2CppSystem.Collections.Generic.List<string>();
#else
            var command = new ClearTrash();
            var args = new List<string>();
#endif

            command.Execute(args);
        }
        
        /// <summary>
        /// Clears the player's wanted level.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void ClearWanted()
        {
#if (IL2CPPMELON || IL2CPPBEPINEX)
            var command = new ClearWanted();
            var args = new Il2CppSystem.Collections.Generic.List<string>();
#else
            var command = new ClearWanted();
            var args = new List<string>();
#endif

            command.Execute(args);
        }
        
        /// <summary>
        /// Adds the given amount of XP to the player.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="amount">The amount of XP to give. Must be a non-negative amount.</param>
        public static void GiveXp(int amount)
        {
#if (IL2CPPMELON || IL2CPPBEPINEX)
            var command = new GiveXP();
            var args = new Il2CppSystem.Collections.Generic.List<string>();
#else
            var command = new GiveXP();
            var args = new List<string>();
#endif
            
            args.Add(amount.ToString());

            command.Execute(args);
        }
        
        /// <summary>
        /// Instantly sets all plants in the world to fully grown.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void GrowPlants()
        {
#if (IL2CPPMELON || IL2CPPBEPINEX)
            var command = new GrowPlants();
            var args = new Il2CppSystem.Collections.Generic.List<string>();
#else
            var command = new GrowPlants();
            var args = new List<string>();
#endif

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

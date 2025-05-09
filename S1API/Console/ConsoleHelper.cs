#if (IL2CPPMELON)
using static Il2CppScheduleOne.Console;
using Il2CppScheduleOne.Employees;
using Il2CppScheduleOne.ItemFramework;
using Il2CppSystem.Collections.Generic;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using static ScheduleOne.Console;
using System.Collections.Generic;
using ScheduleOne.Employees;
using ScheduleOne.ItemFramework;
#endif
using S1API.Internal.Utils;
using S1API.NPCs;
using S1API.Products.Packaging;
using S1API.Property;
using S1API.Quests.Constants;
using S1API.Vehicles;
using System.Globalization;

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
            var command = new AddEmployeeCommand();
            var args = new List<string>();

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
            var command = new AddItemToInventoryCommand();
            var args = new List<string>();

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
            var command = new ChangeOnlineBalanceCommand();
            var args = new List<string>();

            args.Add(amount.ToString());

            command.Execute(args);
        }
        
        /// <summary>
        /// Clears the player's inventory.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void ClearInventory()
        {
            var command = new ClearInventoryCommand();
            var args = new List<string>();

            command.Execute(args);
        }
        
        /// <summary>
        /// Clears all trash from the world.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void ClearTrash()
        {
            var command = new ClearTrash();
            var args = new List<string>();

            command.Execute(args);
        }
        
        /// <summary>
        /// Clears the player's wanted level.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void ClearWanted()
        {
            var command = new ClearWanted();
            var args = new List<string>();

            command.Execute(args);
        }
        
        /// <summary>
        /// Adds the given amount of XP to the player.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="amount">The amount of XP to give. Must be a non-negative amount.</param>
        public static void GiveXp(int amount)
        {
            var command = new GiveXP();
            var args = new List<string>();
            
            args.Add(amount.ToString());

            command.Execute(args);
        }
        
        /// <summary>
        /// Instantly sets all plants in the world to fully grown.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void GrowPlants()
        {
            var command = new GrowPlants();
            var args = new List<string>();

            command.Execute(args);
        }
        
        /// <summary>
        /// Lower the player's wanted level.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void LowerWanted()
        {
            var command = new LowerWanted();
            var args = new List<string>();

            command.Execute(args);
        }
        
        /// <summary>
        /// Packages the currently equipped item into the specified package type.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <remarks>This method can convert between package types. It can change baggies to jars, baggies to bricks, bricks to baggies, etc.</remarks>
        /// <param name="packageType">The type of packaging to package the product as.</param>
        public static void PackageSelectedProduct(PackageType packageType)
        {
            var command = new PackageProduct();
            var args = new List<string>();
            
            args.Add(packageType.ToString());

            command.Execute(args);
        }
        
        /// <summary>
        /// Raise the player's wanted level.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void RaiseWanted()
        {
            var command = new RaisedWanted();
            var args = new List<string>();

            command.Execute(args);
        }
        
        /// <summary>
        /// Saves the player's game.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void SaveGame()
        {
            var command = new Save();
            var args = new List<string>();

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets a product as discovered.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void DiscoverProduct(string productName)
        {
            var command = new SetDiscovered();
            var args = new List<string>();
            
            args.Add(productName);

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets the player's energy level.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="amount">The level of energy to set to. Range is 0 to 100.</param>
        public static void SetPlayerEnergyLevel(float amount)
        {
            var command = new SetEnergy();
            var args = new List<string>();
            
            args.Add(amount.ToString(CultureInfo.InvariantCulture));

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets the player's health.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="amount">The health value to set to.</param>
        public static void SetPlayerHealth(float amount)
        {
            var command = new SetHealth();
            var args = new List<string>();
            
            args.Add(amount.ToString(CultureInfo.InvariantCulture));

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets the player's jump multiplier.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="amount">The multiplier to set to. Must be non-negative.</param>
        public static void SetPlayerJumpMultiplier(float amount)
        {
            var command = new SetJumpMultiplier();
            var args = new List<string>();
            
            args.Add(amount.ToString(CultureInfo.InvariantCulture));

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets the intensity of law enforcement activity.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="amount">The level of activity. Ranges from 0 to 10.</param>
        public static void SetLawIntensity(float amount)
        {
            var command = new SetLawIntensity();
            var args = new List<string>();
            
            args.Add(amount.ToString(CultureInfo.InvariantCulture));

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets the player's move speed multiplier.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="amount">The multiplier to set to. Must be non-negative.</param>
        public static void SetPlayerMoveSpeedMultiplier(float amount)
        {
            var command = new SetMoveSpeedCommand();
            var args = new List<string>();
            
            args.Add(amount.ToString(CultureInfo.InvariantCulture));

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets a business as owned.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="business">The business to set as owned.</param>
        public static void SetBusinessAsOwned(BusinessType business)
        {
            var command = new SetPropertyOwned();
            var args = new List<string>();
            
            args.Add(business.ToString());

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets a property as owned.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="property">The property to set as owned.</param>
        public static void SetPropertyAsOwned(PropertyType property)
        {
            var command = new SetPropertyOwned();
            var args = new List<string>();
            
            args.Add(property.ToString());

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets the equipped product's quality.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="quality">The quality to set the current equipped item to.</param>
        public static void SetQuality(EQuality quality)
        {
            var command = new SetQuality();
            var args = new List<string>();
            
            args.Add(quality.ToString());

            command.Execute(args);
        }

        /// <summary>
        /// Sets the state for a given quest.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="quest">The quest to set the state for.</param>
        /// <param name="state">The state to set for the quest.</param>
        public static void SetQuestState(string quest, QuestState state)
        {
            var command = new SetQuestState();
            var args = new List<string>();
            
            args.Add(quest);
            args.Add(state.ToString());

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets the state for a given quest.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="quest">The quest to set the state for.</param>
        /// <param name="state">The state to set for the quest.</param>
        public static void SetQuestState(VanillaQuest quest, QuestState state)
        {
            var command = new SetQuestState();
            var args = new List<string>();
            
            args.Add(quest.GetDescriptionValue());
            args.Add(state.ToString());

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets the relationship for a given NPC.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="npcId">The ID of the NPC to set the relationship for.</param>
        /// <param name="level">The relationship value to set. Must be between 0 and 5 inclusive.</param>
        public static void SetNpcRelationship(string npcId, float level)
        {
            var command = new SetRelationship();
            var args = new List<string>();
            
            args.Add(npcId);
            args.Add(level.ToString(CultureInfo.InvariantCulture));

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets the relationship for a given NPC.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="npc">The NPC to set the relationship for.</param>
        /// <param name="level">The relationship value to set. Must be between 0 and 5 inclusive.</param>
        public static void SetNpcRelationship(VanillaNpcList npc, float level)
        {
            var command = new SetRelationship();
            var args = new List<string>();
            
            args.Add(npc.GetDescriptionValue());
            args.Add(level.ToString(CultureInfo.InvariantCulture));

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets the time.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="time">The time to set. Requires a valid value with Regex <c>^([01]?[0-9]|2[0-3])[0-5][0-9]$</c> (e.g. 1530 for 15:30 / 3:30 PM)</param>
        public static void SetTime(string time)
        {
            var command = new SetTimeCommand();
            var args = new List<string>();
            
            args.Add(time);

            command.Execute(args);
        }
        
        /// <summary>
        /// Spawns a vehicle at the player's location.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="vehicle">The vehicle to spawn.</param>
        public static void SpawnVehicle(string vehicle)
        {
            var command = new SpawnVehicleCommand();
            var args = new List<string>();
            
            args.Add(vehicle);

            command.Execute(args);
        }
        
        /// <summary>
        /// Spawns a vehicle at the player's location.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="vehicle">The vehicle to spawn.</param>
        public static void SpawnVehicle(VanillaVehicleList vehicle)
        {
            var command = new SpawnVehicleCommand();
            var args = new List<string>();
            
            args.Add(vehicle.ToString());

            command.Execute(args);
        }
        
        /// <summary>
        /// Unlocks the connection for the given NPC.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="npcId">The ID of the NPC to set the relationship for.</param>
        public static void UnlockNpc(string npcId)
        {
            var command = new SetUnlocked();
            var args = new List<string>();
            
            args.Add(npcId);

            command.Execute(args);
        }
        
        /// <summary>
        /// Unlocks the connection for the given NPC.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="npc">The NPC to set the relationship for.</param>
        public static void UnlockNpc(VanillaNpcList npc)
        {
            var command = new SetUnlocked();
            var args = new List<string>();
            
            args.Add(npc.GetDescriptionValue());

            command.Execute(args);
        }
        
        /// <summary>
        /// Teleport to a business.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="business">The business to teleport to.</param>
        public static void TeleportToBusiness(BusinessType business)
        {
            var command = new Teleport();
            var args = new List<string>();
            
            args.Add(business.ToString());

            command.Execute(args);
        }
        
        /// <summary>
        /// Teleport to a property.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="property">The property to teleport to.</param>
        public static void TeleportToProperty(PropertyType property)
        {
            var command = new Teleport();
            var args = new List<string>();
            
            args.Add(property.ToString());

            command.Execute(args);
        }
        
        /// <summary>
        /// Executes the ChangeCashCommand with the given amount.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="amount">The cash amount to add or remove.</param>
        public static void RunCashCommand(int amount)
        {
            var command = new ChangeCashCommand();
            var args = new List<string>();
            
            args.Add(amount.ToString());
            command.Execute(args);
        }
    }
}

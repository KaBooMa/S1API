#if (IL2CPPMELON)
using Il2CppScheduleOne;
using S1ItemFramework = Il2CppScheduleOne.ItemFramework;
using S1MoneyFramework = Il2CppScheduleOne.Money;
#else
using ScheduleOne;
using S1ItemFramework = ScheduleOne.ItemFramework;
using S1MoneyFramework = ScheduleOne.Money;
#endif

using S1API.Internal.Utils;
using System;
using UnityEngine;

namespace S1API.Money
{
    /// <summary>
    /// Provides static access to financial operations, including methods for managing cash balance,
    /// creating online transactions, and calculating net worth.
    /// </summary>
    public static class Money
    {
        /// <summary>
        /// Provides internal access to the underlying financial operations manager.
        /// This property is utilized for interacting with the core financial functionality.
        /// </summary>
        private static S1MoneyFramework.MoneyManager Internal => S1MoneyFramework.MoneyManager.Instance;

        /// <summary>
        /// Event triggered whenever there is a change in the balance,
        /// including cash balance or online transactions.
        /// </summary>
        public static event Action? OnBalanceChanged;

        /// <summary>
        /// Adjusts the cash balance by the specified amount.
        /// </summary>
        /// <param name="amount">The amount to modify the cash balance by. Positive values increase the balance, and negative values decrease it.</param>
        /// <param name="visualizeChange">Indicates whether the cash change should be visualized on the HUD.</param>
        /// <param name="playCashSound">Indicates whether a sound effect should be played to signify the cash adjustment.</param>
        public static void ChangeCashBalance(float amount, bool visualizeChange = true, bool playCashSound = false)
        {
            Internal?.ChangeCashBalance(amount, visualizeChange, playCashSound);
            OnBalanceChanged?.Invoke();
        }

        /// <summary>
        /// Creates an online transaction.
        /// </summary>
        /// <param name="transactionName">The name of the transaction.</param>
        /// <param name="unitAmount">The monetary amount per unit involved in the transaction.</param>
        /// <param name="quantity">The number of units in the transaction.</param>
        /// <param name="transactionNote">An optional note or description for the transaction.</param>
        public static void CreateOnlineTransaction(string transactionName, float unitAmount, float quantity,
            string transactionNote)
        {
            Internal?.CreateOnlineTransaction(transactionName, unitAmount, quantity, transactionNote);
            OnBalanceChanged?.Invoke();
        }

        /// <summary>
        /// Retrieves the total net worth, including all cash and online balances combined.
        /// </summary>
        /// <returns>The total net worth as a floating-point value.</returns>
        public static float GetNetWorth()
        {
            return Internal != null ? Internal.GetNetWorth() : 0f;
        }

        /// <summary>
        /// Retrieves the current cash balance.
        /// </summary>
        /// <returns>The current cash balance as a floating-point value.</returns>
        public static float GetCashBalance()
        {
            return Internal != null ? Internal.cashBalance : 0f;
        }

        /// <summary>
        /// Retrieves the current online balance.
        /// </summary>
        /// <returns>The current amount of online balance.</returns>
        public static float GetOnlineBalance()
        {
            return Internal != null ? Internal.sync___get_value_onlineBalance() : 0f;
        }

        /// <summary>
        /// Registers a callback to be invoked during net worth calculation.
        /// </summary>
        /// <param name="callback">The callback to be executed when net worth is calculated. It receives an object as its parameter.</param>
        public static void AddNetworthCalculation(System.Action<object> callback)
        {
            if (Internal != null)
                Internal.onNetworthCalculation += callback;
        }

        /// <summary>
        /// Removes a previously registered networth calculation callback.
        /// </summary>
        /// <param name="callback">The callback to be removed from the networth calculation updates.</param>
        public static void RemoveNetworthCalculation(System.Action<object> callback)
        {
            if (Internal != null)
                Internal.onNetworthCalculation -= callback;
        }

        /// <summary>
        /// Creates a new cash instance with the specified balance.
        /// </summary>
        /// <param name="amount">The initial amount of cash to set in the instance.</param>
        /// <returns>A newly created instance of cash with the specified balance.</returns>
        public static CashInstance CreateCashInstance(float amount)
        {
#if (IL2CPPMELON)
            var cashItem = Registry.GetItem<Il2CppScheduleOne.ItemFramework.CashDefinition>("cash");
            var instance = CrossType.As<Il2CppScheduleOne.ItemFramework.ItemInstance>(cashItem.GetDefaultInstance(1));
            var cashInstance = new CashInstance(instance);
            cashInstance.SetQuantity(amount);
            return cashInstance;
#else
            var cashItem = Registry.GetItem<S1ItemFramework.CashDefinition>("cash");
            var instance = CrossType.As<S1ItemFramework.ItemInstance>(cashItem.GetDefaultInstance(1));
            var cashInstance = new CashInstance(instance);
            cashInstance.SetQuantity(amount);
            return cashInstance;

#endif
        }
    }
}

#if (IL2CPPMELON)
using S1ItemFramework = Il2CppScheduleOne.ItemFramework;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1ItemFramework = ScheduleOne.ItemFramework;
#endif

using S1API.Internal.Utils;
using S1API.Items;
using UnityEngine;

namespace S1API.Money
{
    /// <summary>
    /// Represents an instance of cash within the game.
    /// </summary>
    public class CashInstance : ItemInstance
    {
        /// <summary>
        /// INTERNAL: The reference to the instanced cash in-game.
        /// </summary>
        internal S1ItemFramework.CashInstance S1CashInstance =>
            CrossType.As<S1ItemFramework.CashInstance>(S1ItemInstance);

        /// <summary>
        /// INTERNAL: Creates an instance of cash from the in-game item instance.
        /// </summary>
        /// <param name="itemInstance"></param>
        internal CashInstance(S1ItemFramework.ItemInstance itemInstance) : base(itemInstance) { }

        /// <summary>
        /// Adds to the quantity of cash for this instance.
        /// NOTE: Supports negative numbers to remove.
        /// </summary>
        /// <param name="amount">Quantity to set the cash to.</param>
        public void AddQuantity(float amount) =>
            S1CashInstance.SetBalance(Mathf.Clamp(S1CashInstance.Balance + amount, 0, float.MaxValue));

        /// <summary>
        /// Sets the quantity of cash for this instance.
        /// </summary>
        /// <param name="newQuantity">Quantity to set the cash to.</param>
        public void SetQuantity(float newQuantity) =>
            S1CashInstance.SetBalance(newQuantity);
    }
}

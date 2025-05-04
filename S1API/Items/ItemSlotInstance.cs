#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1ItemFramework = Il2CppScheduleOne.ItemFramework;
using S1Product = Il2CppScheduleOne.Product;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1ItemFramework = ScheduleOne.ItemFramework;
using S1Product = ScheduleOne.Product;
#endif

using S1API.Internal.Utils;
using S1API.Money;
using S1API.Products;

namespace S1API.Items
{
    /// <summary>
    /// Represents an item slot within the game.
    /// These are present within storage, the hot bar, etc.
    /// </summary>
    public class ItemSlotInstance
    {
        /// <summary>
        /// INTERNAL: The reference to the item slot in the game.
        /// </summary>
        internal readonly S1ItemFramework.ItemSlot S1ItemSlot;

        /// <summary>
        /// Creates an item slot instance from the in game slot.
        /// </summary>
        /// <param name="itemSlot"></param>
        internal ItemSlotInstance(S1ItemFramework.ItemSlot itemSlot) =>
            S1ItemSlot = itemSlot;

        /// <summary>
        /// The quantity of item in this slot.
        /// </summary>
        public int Quantity =>
            S1ItemSlot.Quantity;

        /// <summary>
        /// The item instance the slot contains.
        /// </summary>
        public ItemInstance? ItemInstance
        {
            get
            {
                if (CrossType.Is(S1ItemSlot.ItemInstance, out S1Product.ProductItemInstance productItemInstance))
                {
	                return new ProductInstance(productItemInstance);
                }

                if (CrossType.Is(S1ItemSlot.ItemInstance, out S1ItemFramework.CashInstance cashInstance))
                {
	                return new CashInstance(cashInstance);
                }

                if (CrossType.Is(S1ItemSlot.ItemInstance, out S1ItemFramework.ItemInstance itemInstance))
                {
	                return new ItemInstance(itemInstance);
                }

                return null;
            }
        }

        /// <summary>
        /// Adds a quantity to the item in this slot.
        /// NOTE: Negative numbers are supported and allowed.
        /// </summary>
        /// <param name="amount"></param>
        public void AddQuantity(int amount) =>
            S1ItemSlot.ChangeQuantity(amount);
    }
}

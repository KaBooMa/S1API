#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1 = Il2CppScheduleOne;
using S1ItemFramework = Il2CppScheduleOne.ItemFramework;
using S1Product = Il2CppScheduleOne.Product;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1 = ScheduleOne;
using S1ItemFramework = ScheduleOne.ItemFramework;
using S1Product = ScheduleOne.Product;
#endif

using S1API.Internal.Utils;
using S1API.Money;
using S1API.Products;

namespace S1API.Items
{
    /// <summary>
    /// Provides access to managing items across the game.
    /// </summary>
    public static class ItemManager
    {
        /// <summary>
        /// Gets the definition of an item by its ID.
        /// </summary>
        /// <param name="itemID">The ID of the item.</param>
        /// <returns>An instance of the item definition.</returns>
        public static ItemDefinition GetItemDefinition(string itemID)
        {
            S1ItemFramework.ItemDefinition itemDefinition = S1.Registry.GetItem(itemID);

            if (CrossType.Is(itemDefinition,
                    out S1Product.ProductDefinition productDefinition))
                return new ProductDefinition(productDefinition);

            if (CrossType.Is(itemDefinition,
                    out S1ItemFramework.CashDefinition cashDefinition))
                return new CashDefinition(cashDefinition);

            return new ItemDefinition(itemDefinition);
        }
    }
}

#if (IL2CPPMELON)
using S1Product = Il2CppScheduleOne.Product;
using Il2CppSystem.Collections.Generic;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1Product = ScheduleOne.Product;
#endif
using System.Linq;

namespace S1API.Products
{
    /// <summary>
    /// Provides management over all products in the game.
    /// </summary>
    public static class ProductManager
    {
        /// <summary>
        /// A list of product definitions discovered on this save.
        /// </summary>
        public static ProductDefinition[] DiscoveredProducts => S1Product.ProductManager.DiscoveredProducts.ToArray()
            .Select(productDefinition => ProductDefinitionWrapper.Wrap(new ProductDefinition(productDefinition)))
            .ToArray();

    }
}

#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1Product = Il2CppScheduleOne.Product;
using Il2CppScheduleOne.ItemFramework;

#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1Product = ScheduleOne.Product;
using ScheduleOne.ItemFramework;
#endif

using System.Collections.Generic;
using S1API.Internal.Utils;
using ItemInstance = S1API.Items.ItemInstance;

namespace S1API.Products
{
    /// <summary>
    /// Represents an instance of a product in the game.
    /// </summary>
    public class ProductInstance : ItemInstance
    {
        /// <summary>
        /// INTERNAL: The stored reference to the in-game product instance.
        /// </summary>
        internal S1Product.ProductItemInstance S1ProductInstance =>
            CrossType.As<S1Product.ProductItemInstance>(S1ItemInstance);

        /// <summary>
        /// INTERNAL: Creates a product instance from the in-game product instance.
        /// </summary>
        /// <param name="productInstance"></param>
        internal ProductInstance(S1Product.ProductItemInstance productInstance) : base(productInstance)
        {
        }

        /// <summary>
        /// Whether this product is currently packaged or not.
        /// </summary>
        public bool IsPackaged =>
            S1ProductInstance.AppliedPackaging;

        /// <summary>
        /// The type of packaging applied to this product.
        /// </summary>
        public PackagingDefinition AppliedPackaging =>
            new PackagingDefinition(S1ProductInstance.AppliedPackaging);

        /// <summary>
        /// The quality of this product instance.
        /// </summary>
        public Quality Quality => S1ProductInstance.Quality.ToAPI();

        // Expose the underlying definition's properties (if S1ProductInstance.Definition is available)
        public IReadOnlyList<Properties.Property> Properties => Definition.Properties;

        // Add Definition property if you don't have one yet
        public ProductDefinition Definition => new ProductDefinition(S1ProductInstance.Definition);
    }
}

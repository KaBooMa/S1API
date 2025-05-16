#if (IL2CPPMELON )
using S1Product = Il2CppScheduleOne.Product;
using Properties = Il2CppScheduleOne.Properties;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1Product = ScheduleOne.Product;
using Properties = ScheduleOne.Properties;
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
        /// INTERNAL: Reference to the in-game product item instance.
        /// </summary>
        internal S1Product.ProductItemInstance S1ProductInstance =>
            CrossType.As<S1Product.ProductItemInstance>(S1ItemInstance);

        /// <summary>
        /// Represents an instance of a product derived from an in-game product item instance.
        /// </summary>
        internal ProductInstance(S1Product.ProductItemInstance productInstance) : base(productInstance)
        {
        }

        /// <summary>
        /// Indicates whether the product instance has applied packaging.
        /// </summary>
        public bool IsPackaged =>
            S1ProductInstance.AppliedPackaging;

        /// <summary>
        /// Represents the packaging details currently applied to the product instance, if any.
        /// </summary>
        public PackagingDefinition AppliedPackaging =>
            new PackagingDefinition(S1ProductInstance.AppliedPackaging);

        /// <summary>
        /// Represents the quality tier of the product instance.
        /// </summary>
        /// <remarks>
        /// The quality indicates the standard or grade of the product, ranging through predefined levels such as Trash, Poor, Standard, Premium, and Heavenly.
        /// </remarks>
        public Quality Quality => S1ProductInstance.Quality.ToAPI();

        // Expose the underlying definition's properties (if S1ProductInstance.Definition is available)

        // Add Definition property if you don't have one yet

#if IL2CPPMELON
        public IReadOnlyList<Properties.Property> Properties => Definition.Properties;
        public ProductDefinition Definition => new ProductDefinition(S1ProductInstance.Definition);
#else
        /// <summary>
        /// Represents the collection of properties associated with the product.
        /// </summary>
        public IReadOnlyList<Properties.Property> Properties => Definition.Properties;

        /// <summary>
        /// Represents the definition of a product in the game, including its core properties.
        /// </summary>
        public ProductDefinition Definition => new ProductDefinition(S1ProductInstance.Definition);

#endif

    }
}

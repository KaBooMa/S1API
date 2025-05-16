#if (IL2CPPMELON )
using S1Product = Il2CppScheduleOne.Product;
using S1Properties = Il2CppScheduleOne.Properties;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1Product = ScheduleOne.Product;
using Properties = ScheduleOne.Properties;
#endif
using System.Collections.Generic;
using S1API.Internal.Utils;
using S1ItemInstance = S1API.Items.ItemInstance;
namespace S1API.Products
{
    /// <summary>
    /// Represents an instance of a product in the game.
    /// </summary>
    /// <remarks>
    /// This class defines specific properties and behaviors for a product instance,
    /// such as quality, packaging, and definition, derived from the S1API's item instance structure.
    /// </remarks>
    public class ProductInstance : S1ItemInstance
    {
        /// <summary>
        /// INTERNAL: Provides access to the underlying in-game product item instance.
        /// </summary>
        internal S1Product.ProductItemInstance S1ProductInstance =>
            CrossType.As<S1Product.ProductItemInstance>(S1ItemInstance);

        /// <summary>
        /// Represents an instance of a product, derived from a specific in-game product item instance,
        /// with additional properties for packaging, quality, and product definition.
        /// </summary>
        internal ProductInstance(S1Product.ProductItemInstance productInstance)
            : base(productInstance)
        {
        }

        /// <summary>
        /// Indicates whether the product instance has applied packaging.
        /// </summary>
        public bool IsPackaged => S1ProductInstance.AppliedPackaging;

        /// <summary>
        /// Provides access to the packaging information applied to the product,
        /// represented as a specific packaging definition instance.
        /// </summary>
        public PackagingDefinition AppliedPackaging =>
            new PackagingDefinition(S1ProductInstance.AppliedPackaging);

        /// <summary>
        /// Represents the quality level of the product instance.
        /// </summary>
        /// <remarks>
        /// Quality levels provide a measure of the product's grading, ranging from "Trash" to "Heavenly".
        /// </remarks>
        public Quality Quality => S1ProductInstance.Quality.ToAPI();

        /// <summary>
        /// Gets the definition of the product associated with this instance.
        /// </summary>
        public ProductDefinition Definition => new ProductDefinition(S1ProductInstance.Definition);

        /// <summary>
        /// Gets the list of properties associated with the product definition.
        /// </summary>
        /// <remarks>
        /// This property provides an unmodifiable list of properties associated
        /// with the underlying product definition. Each property represents
        /// a specific characteristic or behavior of the corresponding product.
        /// </remarks>
        public IReadOnlyList<S1Properties.Property> Properties => Definition.Properties;
    }
}

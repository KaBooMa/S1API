#if (IL2CPPMELON)
using S1Product = Il2CppScheduleOne.Product;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1Product = ScheduleOne.Product;
#endif

using S1API.Internal.Utils;
using S1API.Items;

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
        internal ProductInstance(S1Product.ProductItemInstance productInstance) : base(productInstance) { }

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
    }
}

#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.Product;
using S1CocaineDefinition = Il2CppScheduleOne.Product.CocaineDefinition;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using ScheduleOne.Product;
using S1CocaineDefinition = ScheduleOne.Product.CocaineDefinition;
#endif

using S1API.Internal.Utils;
using S1API.Items;

namespace S1API.Products
{
    /// <summary>
    /// Represents the definition of a Cocaine product.
    /// </summary>
    public class CocaineDefinition : ProductDefinition
    {
        /// <summary>
        /// INTERNAL: Strongly typed access to the CocaineDefinition.
        /// </summary>
        internal S1CocaineDefinition S1CocaineDefinition =>
            CrossType.As<S1CocaineDefinition>(S1ItemDefinition);

        /// <summary>
        /// Creates a new cocaine product definition.
        /// </summary>
        /// <param name="definition">The original in-game cocaine definition.</param>
        internal CocaineDefinition(S1CocaineDefinition definition)
            : base(definition) { }

        /// <summary>
        /// Creates an instance of this cocaine product.
        /// </summary>
        public override ItemInstance CreateInstance(int quantity = 1) =>
            new ProductInstance(CrossType.As<ProductItemInstance>(
                S1CocaineDefinition.GetDefaultInstance(quantity)));
    }
}

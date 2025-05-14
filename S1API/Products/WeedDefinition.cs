#if (IL2CPPMELON)
using Il2CppScheduleOne.Product;
using S1WeedDefinition = Il2CppScheduleOne.Product.WeedDefinition;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using ScheduleOne.Product;
using S1WeedDefinition = ScheduleOne.Product.WeedDefinition;
#endif

using S1API.Internal.Utils;
using S1API.Items;

namespace S1API.Products
{
    /// <summary>
    /// Represents the definition of a Weed product.
    /// </summary>
    public class WeedDefinition : ProductDefinition
    {
        /// <summary>
        /// INTERNAL: Strongly typed access to the WeedDefinition.
        /// </summary>
        internal S1WeedDefinition S1WeedDefinition =>
            CrossType.As<S1WeedDefinition>(S1ItemDefinition);


        /// <summary>
        /// Creates a new weed product definition.
        /// </summary>
        /// <param name="definition">The original in-game weed definition.</param>
        internal WeedDefinition(S1WeedDefinition definition)
            : base(definition) { }

        /// <summary>
        /// Creates an instance of this weed product.
        /// </summary>
        public override ItemInstance CreateInstance(int quantity = 1) =>
            new ProductInstance(CrossType.As<ProductItemInstance>(
                S1WeedDefinition.GetDefaultInstance(quantity)));
    }
}

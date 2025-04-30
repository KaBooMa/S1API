#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.Product;
using S1MethDefinition = Il2CppScheduleOne.Product.MethDefinition;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using ScheduleOne.Product;
using S1MethDefinition = ScheduleOne.Product.MethDefinition;
#endif

using S1API.Internal.Utils;
using S1API.Items;

namespace S1API.Products
{
    /// <summary>
    /// Represents the definition of a Meth product.
    /// </summary>
    public class MethDefinition : ProductDefinition
    {
        /// <summary>
        /// INTERNAL: Strongly typed access to the MethDefinition.
        /// </summary>
        internal S1MethDefinition S1MethDefinition =>
            CrossType.As<S1MethDefinition>(S1ItemDefinition);

        /// <summary>
        /// Creates a new meth product definition.
        /// </summary>
        /// <param name="definition">The original in-game meth definition.</param>
        internal MethDefinition(S1MethDefinition definition)
            : base(definition) { }

        /// <summary>
        /// Creates an instance of this meth product.
        /// </summary>
        public override ItemInstance CreateInstance(int quantity = 1) =>
            new ProductInstance(CrossType.As<ProductItemInstance>(
                S1MethDefinition.GetDefaultInstance(quantity)));
    }
}

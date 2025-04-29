using S1API.Internal.Utils;

#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1Product = Il2CppScheduleOne.Product;

#else
using S1Product = ScheduleOne.Product;
#endif

namespace S1API.Products
{
    /// <summary>
    /// Provides functionality to wrap and convert generic product definitions into their specific type-derived definitions.
    /// </summary>
    public static class ProductDefinitionWrapper
    {
        /// <summary>
        /// Converts a generic <see cref="ProductDefinition"/> into its corresponding typed wrapper.
        /// </summary>
        /// <param name="def">The raw product definition to be processed and converted.</param>
        /// <returns>A wrapped instance of <see cref="ProductDefinition"/> with type-specific methods and properties, or the input definition if no specific wrapper applies.</returns>
        public static ProductDefinition Wrap(ProductDefinition def)
        {
            var item = def.S1ItemDefinition;
            if (CrossType.Is<S1Product.WeedDefinition>(item, out var weed))
                return new WeedDefinition(weed);

            if (CrossType.Is<S1Product.MethDefinition>(item, out var meth))
                return new MethDefinition(meth);

            if (CrossType.Is<S1Product.CocaineDefinition>(item, out var coke))
                return new CocaineDefinition(coke);

            return def;
        }
    }
}

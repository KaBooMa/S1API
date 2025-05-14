using S1API.Internal.Utils;

#if (IL2CPPMELON)
using S1Product = Il2CppScheduleOne.Product;
#else
using S1Product = ScheduleOne.Product;
#endif

namespace S1API.Products
{
    /// <summary>
    /// INTERNAL: A wrapper class for converting a product definition to its proper dedicated class.
    /// </summary>
    internal static class ProductDefinitionWrapper
    {
        internal static ProductDefinition Wrap(ProductDefinition def)
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

#if (IL2CPPMELON)
using Il2CppScheduleOne.Product;
using S1MethDefinition = Il2CppScheduleOne.Product.MethDefinition;
    using Properties = Il2CppScheduleOne.Properties;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using ScheduleOne.Product;
using S1MethDefinition = ScheduleOne.Product.MethDefinition;
using Properties = ScheduleOne.Properties;
#endif

using System.Collections.Generic;
using S1API.Internal.Utils;
using S1API.Items;

namespace S1API.Products
{
    /// <summary>
    /// Represents the definition of a Meth product in the product framework.
    /// </summary>
    /// <remarks>
    /// Provides methods for retrieving properties and creating instances of meth products. This class extends the base functionality provided by <see cref="ProductDefinition"/>.
    /// </remarks>
    public class MethDefinition : ProductDefinition
    {
        /// <summary>
        /// INTERNAL: Strongly typed access to S1MethDefinition, representing the Il2CppScheduleOne.Product.MethDefinition entity.
        /// </summary>
        internal S1MethDefinition S1MethDefinition =>
            CrossType.As<S1MethDefinition>(S1ItemDefinition);

        /// <summary>
        /// Represents the definition of a Meth product.
        /// </summary>
        internal MethDefinition(S1MethDefinition definition)
            : base(definition)
        {
        }

        /// <summary>
        /// Creates an instance of this meth product with a specified quantity.
        /// </summary>
        /// <param name="quantity">The quantity of the meth product to instantiate. Defaults to 1 if not provided.</param>
        /// <returns>An instance of the meth product as a <see cref="ItemInstance"/>.</returns>
        public override ItemInstance CreateInstance(int quantity = 1) =>
            new ProductInstance(CrossType.As<ProductItemInstance>(
                S1MethDefinition.GetDefaultInstance(quantity)));

        /// <summary>
        /// Retrieves the list of properties associated with the meth product definition.
        /// </summary>
        /// <returns>A list of properties that belong to the meth product definition.</returns>
#if IL2CPPMELON
        public List<Properties.Property> GetProperties()
#else
        public List<Properties.Property> GetProperties()
#endif
        {
#if IL2CPPMELON
            var result = new List<Properties.Property>();
#else
            var result = new List<Properties.Property>();
#endif
            var list = S1MethDefinition?.Properties;
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    result.Add(list[i]);
                }
            }

            return result;
        }
    }
}

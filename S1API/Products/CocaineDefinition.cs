#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.Product;
using S1CocaineDefinition = Il2CppScheduleOne.Product.CocaineDefinition;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using ScheduleOne.Product;
using S1CocaineDefinition = ScheduleOne.Product.CocaineDefinition;
#endif

using System.Collections.Generic;
using S1API.Internal.Utils;
using S1API.Items;

namespace S1API.Products
{
    /// <summary>
    /// Represents the definition of a cocaine product within the system.
    /// </summary>
    public class CocaineDefinition : ProductDefinition
    {
        /// <summary>
        /// INTERNAL: Strongly typed access to the CocaineDefinition within the Schedule One framework.
        /// </summary>
        internal S1CocaineDefinition S1CocaineDefinition =>
            CrossType.As<S1CocaineDefinition>(S1ItemDefinition);

        /// <summary>
        /// Represents the definition of a Cocaine product.
        /// </summary>
        internal CocaineDefinition(S1CocaineDefinition definition)
            : base(definition)
        {
        }

        /// <summary>
        /// Creates an instance of this cocaine product with the specified quantity.
        /// </summary>
        /// <param name="quantity">The quantity of the product to create. Defaults to 1.</param>
        /// <returns>An instance of the cocaine product with the specified quantity.</returns>
        public override ItemInstance CreateInstance(int quantity = 1) =>
            new ProductInstance(CrossType.As<ProductItemInstance>(
                S1CocaineDefinition.GetDefaultInstance(quantity)));

        /// <summary>
        /// Retrieves a list of properties associated with the current cocaine product definition.
        /// </summary>
        /// <returns>A list of properties specific to the cocaine product definition.</returns>
#if IL2CPPBEPINEX || IL2CPPMELON
        public List<Il2CppScheduleOne.Properties.Property> GetProperties()
#else
        public List<ScheduleOne.Properties.Property> GetProperties()
#endif
        {
#if IL2CPPBEPINEX || IL2CPPMELON
            var result = new List<Il2CppScheduleOne.Properties.Property>();
#else
            var result = new List<ScheduleOne.Properties.Property>();
#endif
            var list = S1CocaineDefinition?.Properties;
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

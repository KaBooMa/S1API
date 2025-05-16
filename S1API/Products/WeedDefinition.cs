#if (IL2CPPMELON)
using Il2CppScheduleOne.Product;
using S1WeedDefinition = Il2CppScheduleOne.Product.WeedDefinition;
using Properties = Il2CppScheduleOne.Properties;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using ScheduleOne.Product;
using S1WeedDefinition = ScheduleOne.Product.WeedDefinition;
using Properties = ScheduleOne.Properties;
#endif

using S1API.Internal.Utils;
using S1API.Items;
using System.Collections.Generic;

namespace S1API.Products
{
    /// <summary>
    /// Represents a specific type of weed product definition. This class extends the functionality of
    /// <see cref="ProductDefinition"/> to include details specific to weed products.
    /// </summary>
    /// <remarks>
    /// This class provides methods and properties to work with weed-related product definitions,
    /// including creating product instances and accessing weed-specific properties.
    /// </remarks>
    public class WeedDefinition : ProductDefinition
    {
        /// <summary>
        /// Represents the definition of a weed product in the ScheduleOne API.
        /// Provides access to underlying data and functionalities specific to weed products.
        /// </summary>
        internal S1WeedDefinition S1WeedDefinition =>
            CrossType.As<S1WeedDefinition>(S1ItemDefinition);

        /// <summary>
        /// Represents a specific type of product definition for weed products in the API.
        /// </summary>
        /// <remarks>
        /// This class acts as a wrapper for `Il2CppScheduleOne.Product.WeedDefinition`,
        /// providing additional functionality and a specific type for handling weed items.
        /// </remarks>
        internal WeedDefinition(S1WeedDefinition definition)
            : base(definition)
        {
        }

        /// <summary>
        /// Creates an instance of the product with the specified quantity.
        /// </summary>
        /// <param name="quantity">The quantity of the product to create. Defaults to 1 if not specified.</param>
        /// <returns>An instance of <see cref="ItemInstance"/> representing the created product.</returns>
        public override ItemInstance CreateInstance(int quantity = 1) =>
            new ProductInstance(CrossType.As<ProductItemInstance>(
                S1WeedDefinition.GetDefaultInstance(quantity)));

        /// <summary>
        /// Gets a list of properties associated with the current weed definition.
        /// </summary>
        /// <returns>
        /// A list of properties of type Il2CppScheduleOne.Properties.Property
        /// associated with the weed definition. If no properties are found,
        /// an empty list is returned.
        /// </returns>
#if IL2CPPMELON
        public List<Properties.Property> GetProperties()
#else
        public List<Properties.Property> GetProperties()
#endif
        {
#if  IL2CPPMELON
            var result = new List<Properties.Property>();
#else
            var result = new List<Properties.Property>();
            #endif
            var list = S1WeedDefinition?.Properties;
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

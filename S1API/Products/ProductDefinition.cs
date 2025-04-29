#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppInterop.Runtime.InteropTypes;
using S1Product = Il2CppScheduleOne.Product;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1Product = ScheduleOne.Product;
#endif

using System.Collections.Generic;
using S1API.Internal.Utils;
using S1API.Items;
using UnityEngine;

namespace S1API.Products
{
    /// <summary>
    /// Represents a product definition in the game.
    /// </summary>
    public class ProductDefinition : ItemDefinition
    {
        /// <summary>
        /// INTERNAL: Stored reference to the game product definition.
        /// </summary>
        internal S1Product.ProductDefinition S1ProductDefinition =>
            CrossType.As<S1Product.ProductDefinition>(S1ItemDefinition);

        /// <summary>
        /// INTERNAL: Creates a product definition from the in-game product definition.
        /// </summary>
        /// <param name="productDefinition"></param>
        internal ProductDefinition(Il2CppScheduleOne.ItemFramework.ItemDefinition productDefinition) : base(productDefinition) { }

        /// <summary>
        /// The price associated with this product.
        /// </summary>
        public float Price =>
            S1ProductDefinition.Price;

        /// <summary>
        /// Creates an instance of this product in-game.
        /// </summary>
        /// <param name="quantity">The quantity of product.</param>
        /// <returns>An instance of the product.</returns>
        public override ItemInstance CreateInstance(int quantity = 1) =>
            new ProductInstance(CrossType.As<S1Product.ProductItemInstance>(S1ProductDefinition.GetDefaultInstance(quantity)));

        /// <summary>
        /// Gets the in-game icon associated with the product.
        /// </summary>
        public Sprite Icon
        {
            get { return S1ProductDefinition.Icon; }
        }

    private List<Properties.Property> properties; // or however properties are stored

    // Add this public property if it doesn't exist yet
    public IReadOnlyList<Properties.Property> Properties => properties.AsReadOnly();
}
}

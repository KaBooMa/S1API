#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1Packaging = Il2CppScheduleOne.Product.Packaging;
using S1ItemFramework = Il2CppScheduleOne.ItemFramework;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1Packaging = ScheduleOne.Product.Packaging;
using S1ItemFramework = ScheduleOne.ItemFramework;
#endif

using S1API.Internal.Utils;
using S1API.Items;

namespace S1API.Products
{
    /// <summary>
    /// Represents a type of packaging in-game.
    /// </summary>
    public class PackagingDefinition : ItemDefinition
    {
        /// <summary>
        /// INTERNAL: A reference to the packaging definition in-game.
        /// </summary>
        internal S1Packaging.PackagingDefinition S1PackagingDefinition =>
            CrossType.As<S1Packaging.PackagingDefinition>(S1ItemDefinition);

        /// <summary>
        /// INTERNAL: Creates an instance of this packaging definition from the in-game packaging definition instance.
        /// </summary>
        /// <param name="s1ItemDefinition"></param>
        internal PackagingDefinition(S1ItemFramework.ItemDefinition s1ItemDefinition) :
            base(s1ItemDefinition) { }

        /// <summary>
        /// The quantity that this packaging can hold.
        /// </summary>
        public int Quantity =>
            S1PackagingDefinition.Quantity;
    }
}

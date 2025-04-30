#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1ItemFramework = Il2CppScheduleOne.ItemFramework;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1ItemFramework = ScheduleOne.ItemFramework;
#endif

using S1API.Internal.Utils;
using S1API.Items;

namespace S1API.Money
{
    /// <summary>
    /// Represents the definition of a cash type.
    /// NOTE: This is not the instance of cash, but the definition itself.
    /// </summary>
    public class CashDefinition : ItemDefinition
    {
        /// <summary>
        /// INTERNAL: A reference to the cash definition in-game.
        /// </summary>
        internal S1ItemFramework.CashDefinition S1CashDefinition =>
            CrossType.As<S1ItemFramework.CashDefinition>(S1ItemDefinition);

        /// <summary>
        /// INTERNAL: Creates a cash definition from the game cash definition.
        /// </summary>
        /// <param name="s1ItemDefinition"></param>
        internal CashDefinition(S1ItemFramework.CashDefinition s1ItemDefinition) : base(s1ItemDefinition) { }

        /// <summary>
        /// Creates an instance of the cash definition in-game.
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public override ItemInstance CreateInstance(int quantity = 1) =>
            new CashInstance(S1CashDefinition.GetDefaultInstance(quantity));
    }
}

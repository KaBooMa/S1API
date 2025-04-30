#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1ItemFramework = Il2CppScheduleOne.ItemFramework;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1ItemFramework = ScheduleOne.ItemFramework;
#endif


namespace S1API.Items
{
    /// <summary>
    /// Represents an item instance in the game.
    /// NOTE: A instance is the item existing in the game world. For example, "I have five sodas in my hand.".
    /// The definition for items in the game will be a <see cref="ItemDefinition"/> instead.
    /// </summary>
    public class ItemInstance
    {
        /// <summary>
        /// INTERNAL: The reference to the instance of this item.
        /// </summary>
        internal readonly S1ItemFramework.ItemInstance S1ItemInstance;

        /// <summary>
        /// INTERNAL: Creates an item instance
        /// </summary>
        /// <param name="itemInstance">The instance of the item instance in-game.</param>
        internal ItemInstance(S1ItemFramework.ItemInstance itemInstance) =>
            S1ItemInstance = itemInstance;

        /// <summary>
        /// The item definition of this item.
        /// </summary>
        public ItemDefinition Definition =>
            new ItemDefinition(S1ItemInstance.Definition);
    }
}

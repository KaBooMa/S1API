#if (IL2CPPMELON)
using S1ItemFramework = Il2CppScheduleOne.ItemFramework;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1ItemFramework = ScheduleOne.ItemFramework;
#endif

using S1API.Items;
using S1API.Internal.Utils;

namespace S1API.Items
{
    /// <summary>
    /// Represents an item instance in the game world (physical item you own).
    /// </summary>
    public class ItemInstance
    {
        /// <summary>
        /// INTERNAL: Reference to the in-game item instance.
        /// </summary>
        internal readonly S1ItemFramework.ItemInstance S1ItemInstance;

        /// <summary>
        /// INTERNAL: Creates an ItemInstance wrapper.
        /// </summary>
        /// <param name="itemInstance">In-game item instance</param>
        internal ItemInstance(S1ItemFramework.ItemInstance itemInstance) =>
            S1ItemInstance = itemInstance;

        // ====== Properties ======

        /// <summary>
        /// The definition (template) this instance was created from.
        /// </summary>
        public ItemDefinition Definition =>
            new ItemDefinition(S1ItemInstance.Definition);

        /// <summary>
        /// Current quantity of this item (stacks).
        /// </summary>
        public int Quantity
        {
            get => S1ItemInstance.Quantity;
            set => S1ItemInstance.SetQuantity(value);
        }

        /// <summary>
        /// Whether this instance is stackable (based on StackLimit).
        /// </summary>
        public bool IsStackable =>
            Definition.StackLimit > 1;
    }
}

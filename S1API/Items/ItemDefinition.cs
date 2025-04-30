#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1ItemFramework = Il2CppScheduleOne.ItemFramework;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1ItemFramework = ScheduleOne.ItemFramework;
#endif

using S1API.Internal.Abstraction;

namespace S1API.Items
{
    /// <summary>
    /// Represents an item definition in-game.
    /// NOTE: A definition is "what" the item is. For example, "This is a `Soda`".
    /// Any instanced items in the game will be a <see cref="ItemInstance"/> instead.
    /// </summary>
    public class ItemDefinition : IGUIDReference
    {
        /// <summary>
        /// INTERNAL: A reference to the item definition in the game.
        /// </summary>
        internal readonly S1ItemFramework.ItemDefinition S1ItemDefinition;

        /// <summary>
        /// Creates a new item definition from the game item definition instance.
        /// </summary>
        /// <param name="s1ItemDefinition"></param>
        internal ItemDefinition(S1ItemFramework.ItemDefinition s1ItemDefinition) =>
            S1ItemDefinition = s1ItemDefinition;

        /// <summary>
        /// INTERNAL: Gets an item definition from a GUID.
        /// </summary>
        /// <param name="guid">The GUID to look for</param>
        /// <returns>The applicable item definition, if found.</returns>
        internal static ItemDefinition GetFromGUID(string guid) =>
            ItemManager.GetItemDefinition(guid);

        /// <summary>
        /// Performs an equals check on the game item definition instance.
        /// </summary>
        /// <param name="obj">The item definition you want to compare against.</param>
        /// <returns>Whether the item definitions are the same or not.</returns>
        public override bool Equals(object? obj) =>
            obj is ItemDefinition other && S1ItemDefinition == other.S1ItemDefinition;

        /// <summary>
        /// Snags the hash code from the game instance versus this instance.
        /// </summary>
        /// <returns>The game intance hash code</returns>
        public override int GetHashCode() =>
            S1ItemDefinition?.GetHashCode() ?? 0;

        /// <summary>
        /// Performs an == check on the game item definition instance.
        /// </summary>
        /// <param name="left">The first item definition to compare.</param>
        /// <param name="right">The second item definition to compare.</param>
        /// <returns>Whether the item definitions are the same or not.</returns>
        public static bool operator ==(ItemDefinition? left, ItemDefinition? right)
        {
            if (ReferenceEquals(left, right)) return true;
            return left?.S1ItemDefinition == right?.S1ItemDefinition;
        }

        /// <summary>
        /// Performs an != check on the game item definition instance.
        /// </summary>
        /// <param name="left">The first item definition to compare.</param>
        /// <param name="right">The second item definition to compare.</param>
        /// <returns>Whether the item definitions are different or not.</returns>
        public static bool operator !=(ItemDefinition left, ItemDefinition right) =>
            !(left == right);

        /// <summary>
        /// The unique identifier assigned to this item definition.
        /// </summary>
        public virtual string GUID =>
            S1ItemDefinition.ID;

        /// <summary>
        /// The unique identifier assigned to this item definition.
        /// </summary>
        public string ID =>
            S1ItemDefinition.ID;

        /// <summary>
        /// The display name for this item.
        /// </summary>
        public string Name =>
            S1ItemDefinition.Name;

        /// <summary>
        /// The description used for this item.
        /// </summary>
        public string Description =>
            S1ItemDefinition.Description;

        /// <summary>
        /// The category this item is assigned to.
        /// </summary>
        public ItemCategory Category =>
            (ItemCategory)S1ItemDefinition.Category;

        /// <summary>
        /// The stack limit for this item.
        /// </summary>
        public int StackLimit =>
            S1ItemDefinition.StackLimit;

        /// <summary>
        /// Creates an instance of this item from the definition.
        /// </summary>
        /// <param name="quantity">How many of the item the instance will have.</param>
        /// <returns>A new item instance within the game.</returns>
        public virtual ItemInstance CreateInstance(int quantity = 1) =>
            new ItemInstance(S1ItemDefinition.GetDefaultInstance(quantity));
    }
}

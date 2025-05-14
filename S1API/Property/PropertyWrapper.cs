using UnityEngine;

namespace S1API.Property
{
    /// <summary>
    /// Represents a wrapper class for handling properties derived from the
    /// Il2CppScheduleOne.Property.Property class. Provides an abstraction for
    /// interacting with property details and operations in Unity.
    /// </summary>
    public class PropertyWrapper : BaseProperty
    {
        /// <summary>
        /// A readonly backing field encapsulating the core property instance
        /// used within the PropertyWrapper class. This field provides access
        /// to the underlying implementation of the property functionalities
        /// and is leveraged across multiple overriden members to delegate
        /// operations to the actual property instance.
        /// </summary>
#if IL2CPPMELON
        internal readonly Il2CppScheduleOne.Property.Property InnerProperty;
#else
        internal readonly ScheduleOne.Property.Property InnerProperty;
#endif
        /// <summary>
        /// A wrapper class that extends the functionality of <see cref="BaseProperty"/>
        /// and acts as a bridge to interact with an inner property implementation
        /// from the Il2CppScheduleOne.Property namespace.
        /// </summary>
#if IL2CPPMELON
        public PropertyWrapper(Il2CppScheduleOne.Property.Property property)
#else
        public PropertyWrapper(ScheduleOne.Property.Property property)
#endif
        {
            InnerProperty = property;
        }

        /// <summary>
        /// Gets the name of the property.
        /// Represents the underlying property name as defined by its implementation.
        /// </summary>
        public override string PropertyName => InnerProperty.PropertyName;

        /// <summary>
        /// Gets the unique code representing this property. This code serves as an identifier
        /// for distinguishing the property in the system and is typically defined in the internal
        /// implementation of the property.
        /// </summary>
        public override string PropertyCode => InnerProperty.PropertyCode;

        /// <summary>
        /// Gets the price of the property.
        /// </summary>
        /// <remarks>
        /// The price represents a floating-point value that denotes the monetary
        /// value or cost associated with the property. This property is read-only
        /// and retrieves the value from the underlying property implementation.
        /// </remarks>
        public override float Price => InnerProperty.Price;

        /// <summary>
        /// Gets a value indicating whether the property is currently owned.
        /// </summary>
        /// <remarks>
        /// This property reflects the ownership status of the property. Returns true if the property
        /// is owned and false otherwise. The ownership status is based on the internal state of the
        /// wrapped property implementation.
        /// </remarks>
        public override bool IsOwned => InnerProperty.IsOwned;

        /// <summary>
        /// Represents the maximum number of employees that can be allocated to the property.
        /// This property is both readable and writable, allowing for dynamic configuration
        /// of employee capacity based on the property's current requirements or constraints.
        /// </summary>
        public override int EmployeeCapacity
        {
            get => InnerProperty.EmployeeCapacity;
            set => InnerProperty.EmployeeCapacity = value;
        }

        /// <summary>
        /// Marks the property as owned within the PropertyWrapper implementation.
        /// Updates the ownership status by delegating the operation to the underlying
        /// Il2CppScheduleOne.Property.Property instance.
        /// This is typically used to signify that the property has been acquired or purchased.
        /// </summary>
        public override void SetOwned()
        {
            InnerProperty.SetOwned();
        }

        /// <summary>
        /// Determines whether a specified point lies within the boundary of the property.
        /// </summary>
        /// <param name="point">The point to check, specified as a Vector3 coordinate.</param>
        /// <returns>true if the point is within the property's boundary; otherwise, false.</returns>
        public override bool IsPointInside(Vector3 point)
        {
            return InnerProperty.DoBoundsContainPoint(point);
        }
    }
}

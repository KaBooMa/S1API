namespace S1API.Property
{
    /// <summary>
    /// Represents an abstract base class for properties in the system.
    /// </summary>
    public abstract class BaseProperty
    {
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <remarks>
        /// This property represents the name or title of the property entity.
        /// It retrieves the value associated with the property from the underlying system or data structure.
        /// </remarks>
        public abstract string PropertyName { get; }

        /// <summary>
        /// Gets the unique code that identifies the property.
        /// </summary>
        /// <remarks>
        /// This code is typically used to differentiate between various properties
        /// within the system. It is unique to each property and can be leveraged
        /// in operations like identification, filtering, or querying.
        /// </remarks>
        public abstract string PropertyCode { get; }

        /// <summary>
        /// Represents the cost or monetary value associated with a property.
        /// </summary>
        /// <remarks>
        /// The <c>Price</c> property is a floating-point value that indicates the price for the property.
        /// It provides a read-only mechanism to access this value.
        /// This value is essential in determining the economic aspect of the property.
        /// </remarks>
        /// <value>
        /// A <c>float</c> representing the monetary price of the property.
        /// </value>
        public abstract float Price { get; }

        /// Indicates whether the property is currently owned or not.
        /// This property is read-only and reflects the ownership status
        /// of the property.
        public abstract bool IsOwned { get; }

        /// <summary>
        /// Gets or sets the maximum number of employees that can be assigned to the property.
        /// </summary>
        /// <remarks>
        /// This property represents the capacity for employees within a given property.
        /// Modifying this value impacts the operations and resource management of the property.
        /// Suitable for scenarios where resource allocation and workforce planning are essential.
        /// </remarks>
        public abstract int EmployeeCapacity { get; set; }

        /// <summary>
        /// Marks the property as owned. This method updates the ownership status
        /// of the property by interacting with the underlying property implementation.
        /// Typically used to signify that the property has been acquired or purchased.
        /// </summary>
        public abstract void SetOwned();

        /// <summary>
        /// Determines whether a specified point lies within the boundary of the property.
        /// </summary>
        /// <param name="point">The point to check, specified as a Vector3 coordinate.</param>
        /// <returns>true if the point is within the property's boundary; otherwise, false.</returns>
        public abstract bool IsPointInside(UnityEngine.Vector3 point);
    }
}
namespace S1API.Internal.Abstraction
{
    /// <summary>
    /// INTERNAL: Represents a class that should serialize by GUID instead of values directly.
    /// This is important to utilize on instances such as dead drops, item definitions, etc.
    /// </summary>
#pragma warning disable S101
    internal interface IGUIDReference
#pragma warning restore S101
    {
        /// <summary>
        /// The GUID associated with the object.
        /// </summary>
        public string GUID { get; }
    }
}

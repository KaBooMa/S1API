#if IL2CPPBEPINEX || IL2CPPMELON
using InternalQuality = Il2CppScheduleOne.ItemFramework.EQuality;
#else
using InternalQuality = ScheduleOne.ItemFramework.EQuality;
#endif
namespace S1API.Products
{
    /// <summary>
    /// Represents the quality levels for items.
    /// </summary>
    /// <remarks>
    /// This enumeration defines various quality tiers that items can belong to. Each tier represents a specific
    /// standard or grade, ranging from the lowest to the highest.
    /// </remarks>
    public enum Quality
    {
        /// <summary>
        /// Represents the lowest quality level, indicating an item of no value or unusable condition.
        /// </summary>
        Trash = 0,

        /// <summary>
        /// Represents a quality level that is below standard but better than trash-quality.
        /// </summary>
        Poor = 1,

        /// <summary>
        /// Represents a standard level of quality in the predefined quality enumeration.
        /// Typically used to indicate an average or commonly acceptable quality level.
        /// </summary>
        Standard = 2,

        /// <summary>
        /// Represents a higher-tier quality level compared to lower
        Premium = 3,

        /// <summary>
        /// Represents the highest level of quality, denoted as "Heavenly".
        Heavenly = 4
    }

    /// <summary>
    /// Provides extension methods for converting between <see cref="Il2CppScheduleOne.ItemFramework.EQuality"/> and
    /// <see cref="S1API.Products.Quality"/> enumerations.
    /// </summary>
    internal static class QualityExtensions
    {
        /// <summary>
        /// Converts an instance of <see cref="Il2CppScheduleOne.ItemFramework.EQuality"/> to its corresponding
        /// <see cref="S1API.Products.Quality"/> representation.
        /// </summary>
        /// <param name="quality">The <see cref="Il2CppScheduleOne.ItemFramework.EQuality"/> instance to convert.</param>
        /// <returns>A <see cref="S1API.Products.Quality"/> value that represents the converted quality.</returns>
        internal static Quality ToAPI(this InternalQuality quality)
        {
            return quality switch
            {
                InternalQuality.Trash => Quality.Trash,
                InternalQuality.Poor => Quality.Poor,
                InternalQuality.Standard => Quality.Standard,
                InternalQuality.Premium => Quality.Premium,
                InternalQuality.Heavenly => Quality.Heavenly,
                _ => Quality.Trash,
            };
        }

        /// <summary>
        /// Converts an instance of the <see cref="Quality"/> enum to its corresponding
        /// <see cref="InternalQuality"/> enum representation.
        /// </summary>
        /// <param name="quality">The <see cref="Quality"/> enum value to convert.</param>
        /// <returns>The corresponding <see cref="InternalQuality"/> enum value.</returns>
        internal static InternalQuality ToInternal(this Quality quality)
        {
            return quality switch
            {
                Quality.Trash => InternalQuality.Trash,
                Quality.Poor => InternalQuality.Poor,
                Quality.Standard => InternalQuality.Standard,
                Quality.Premium => InternalQuality.Premium,
                Quality.Heavenly => InternalQuality.Heavenly,
                _ => InternalQuality.Trash,
            };
        }
    }
}

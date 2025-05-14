using S1API.Entities.Appearances.Base;

namespace S1API.Entities.Appearances.FaceLayerFields
{
    /// <summary>
    /// The EyeShadow index in AvatarSettings
    /// </summary>
    public class Eyes : BaseFaceAppearance
    {
        public const string EyeShadow = "Avatar/Layers/Face/EyeShadow";
        public const string Freckles = "Avatar/Layers/Face/Freckles";
        public const string OldPersonWrinkles = "Avatar/Layers/Face/OldPersonWrinkles";
        public const string TiredEyes = "Avatar/Layers/Face/TiredEyes";
    }
}

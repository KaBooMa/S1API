#if (IL2CPPMELON)
using S1AvatarFramework = Il2CppScheduleOne.AvatarFramework;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1AvatarFramework = ScheduleOne.AvatarFramework;
#endif

using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

using S1API.Entities.Appearances.AccessoryFields;
using S1API.Entities.Appearances.Base;
using S1API.Entities.Appearances.BodyLayerFields;
using S1API.Entities.Appearances.CustomizationFields;
using S1API.Entities.Appearances.FaceLayerFields;
using S1API.Internal.Utils;
using S1API.Logging;

namespace S1API.Entities
{
    /// <summary>
    ///
    /// </summary>
    public class NPCAppearance
    {
        private static readonly Log _logger = new Log("NPCAppearance");

        #region Internal Members

        /// <summary>
        /// INTERNAL: Reference to the NPC on API side
        /// </summary>
        internal readonly NPC NPC;

        /// <summary>
        /// INTERNAL: Constructor used for assigning the NPC instance.
        /// </summary>
        /// <param name="npc"></param>
        internal NPCAppearance(NPC npc)
        {
            NPC = npc;

            // Defaulting to the local player for Avatar
            NPC.S1NPC.Avatar = S1AvatarFramework.MugshotGenerator.Instance.MugshotRig;

            // Create the new AvatarSettings by default
            _customAvatarSettings = ScriptableObject.CreateInstance<S1AvatarFramework.AvatarSettings>();
            ApplyDefaultSettings(_customAvatarSettings);

            // Assign the appearance for already existing NPCs with existing AvatarSettings
            S1AvatarFramework.AvatarSettings avatarSettings = Resources.Load<S1AvatarFramework.AvatarSettings>($"charactersettings/{NPC.S1NPC.FirstName}");
            NPC.S1NPC.Avatar.LoadAvatarSettings(avatarSettings);
        }

        /// <summary>
        /// Generate the Mugshot for the <see cref="NPC"/> instance
        /// </summary>
        internal void GenerateMugshot()
        {
            // Enable the MugshotRig GameObject, if we do not do this the Mugshots are blank.
            S1AvatarFramework.MugshotGenerator.Instance.MugshotRig.transform.parent.gameObject.SetActive(true);

            // Grab the right AvatarSettings instance to use
            if (!NPC.S1NPC.Avatar.InitialAvatarSettings)
                NPC.S1NPC.Avatar.LoadAvatarSettings(_customAvatarSettings);

            // Generate the Mugshot for the NPC
            NPC.S1NPC.Avatar.GetMugshot((Action<Texture2D>)(generatedMugshot =>
            {
                // Apply the generated Texture2D instance to the GPU
                // Otherwise it'll be blank in Messages App (for example).
                generatedMugshot.Apply();

                // Create the sprite and assign it to the NPC Icon.
                Sprite iconSprite = Sprite.Create(generatedMugshot, new Rect(0, 0, generatedMugshot.width, generatedMugshot.height), Vector2.zero);
                NPC.Icon = iconSprite;
            }));
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Sets an appearance field within <see cref="S1AvatarFramework.AvatarSettings"/>
        /// </summary>
        /// <typeparam name="T">The appearance type</typeparam>
        /// <param name="appearanceValue">The value to set</param>
        public NPCAppearance Set<T>(object appearanceValue) where T : BaseAppearance
        {
            if (_setters.TryGetValue(typeof(T), out var setter))
            {
                try
                {
                    setter(this, appearanceValue);
                }
                catch (Exception ex)
                {
                    _logger.Error($"Failed to set {typeof(T).Name}: {ex.Message}");
                }
            }
            else
                _logger.Error($"No setter registered for appearance type {typeof(T).Name}");

            return this;
        }

        /// <summary>
        /// Adds a Face Layer within the <see cref="S1AvatarFramework.AvatarSettings"/>
        /// </summary>
        /// <param name="path">The asset path</param>
        /// <param name="hexColor">The color in Hexadecimals</param>
        /// <typeparam name="T"></typeparam>
        public NPCAppearance WithFaceLayer<T>(string path, uint hexColor) where T : BaseFaceAppearance =>
            WithFaceLayer<T>(path, hexColor.ToColor());

        /// <summary>
        /// Adds a Face Layer within the <see cref="S1AvatarFramework.AvatarSettings"/>
        /// </summary>
        /// <param name="path">The asset path</param>
        /// <param name="color">The color instance</param>
        /// <typeparam name="T"></typeparam>
        public NPCAppearance WithFaceLayer<T>(string path, Color color) where T : BaseFaceAppearance
        {
            if (_customAvatarSettings.FaceLayerSettings.Count > MaxFaceLayers)
                return this;

            _customAvatarSettings.FaceLayerSettings.Add(new S1AvatarFramework.AvatarSettings.LayerSetting
            {
                layerPath = path,
                layerTint =  color
            });

            return this;
        }

        /// <summary>
        /// Adds a Body Layer within the <see cref="S1AvatarFramework.AvatarSettings"/>
        /// </summary>
        /// <param name="path">The asset path</param>
        /// <param name="hexColor">The color in Hexadecimals</param>
        /// <typeparam name="T"></typeparam>
        public NPCAppearance WithBodyLayer<T>(string path, uint hexColor) where T : BaseBodyAppearance =>
            WithBodyLayer<T>(path, hexColor.ToColor());

        /// <summary>
        /// Adds a Body Layer within the <see cref="S1AvatarFramework.AvatarSettings"/>
        /// </summary>
        /// <param name="path">The asset path</param>
        /// <param name="color">The color instance</param>
        /// <typeparam name="T"></typeparam>
        public NPCAppearance WithBodyLayer<T>(string path, Color color) where T : BaseBodyAppearance
        {
            if (_customAvatarSettings.BodyLayerSettings.Count > MaxBodyLayers)
                return this;

            _customAvatarSettings.BodyLayerSettings.Add(new S1AvatarFramework.AvatarSettings.LayerSetting
            {
                layerPath = path,
                layerTint =  color
            });

            return this;
        }

        /// <summary>
        /// Adds a Accessory Layer within the <see cref="S1AvatarFramework.AvatarSettings"/>
        /// </summary>
        /// <param name="path">The asset path</param>
        /// <param name="hexColor">The color in Hexadecimals</param>
        /// <typeparam name="T"></typeparam>
        public NPCAppearance WithAccessoryLayer<T>(string path, uint hexColor) where T : BaseAccessoryAppearance =>
            WithAccessoryLayer<T>(path, hexColor.ToColor());

        /// <summary>
        /// Adds a Accessory Layer within the <see cref="S1AvatarFramework.AvatarSettings"/>
        /// </summary>
        /// <param name="path">The asset path</param>
        /// <param name="color">The color instance</param>
        /// <typeparam name="T"></typeparam>
        public NPCAppearance WithAccessoryLayer<T>(string path, Color color) where T : BaseAccessoryAppearance
        {
            if (_customAvatarSettings.AccessorySettings.Count > MaxAccessoryLayers)
                return this;

            _customAvatarSettings.AccessorySettings.Add(new S1AvatarFramework.AvatarSettings.AccessorySetting
            {
                path = path,
                color =  color
            });

            return this;
        }

        /// <summary>
        /// Finalizes the appearance by generating the NPC's mugshot.
        /// This can be called after setting all appearance attributes.
        /// </summary>
        /// <returns>The <see cref="NPCAppearance"/> instance with the generated mugshot.</returns>
        public NPCAppearance Build()
        {
            GenerateMugshot();
            return this;
        }

        /// <summary>
        /// Generates a random appearance for the <see cref="NPC"/>
        /// </summary>
        public void GenerateRandomAppearance()
        {
            Color RandomColor() => new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
            float RandomRange(float min, float max) => UnityEngine.Random.Range(min, max);
            string RandomFromList(List<string> list) => list[UnityEngine.Random.Range(0, list.Count)];

            #region Customization Fields

            // Customization fields
            Set<EyeBallTint>(RandomColor());
            Set<EyebrowRestingAngle>(RandomRange(0f, 1f));
            Set<EyebrowRestingHeight>(RandomRange(0f, 1f));
            Set<EyebrowScale>(RandomRange(0f, 1f));
            Set<EyebrowThickness>(RandomRange(0f, 1f));

            float topLid = RandomRange(0f, 1f);
            float bottomLid = RandomRange(0f, 1f);
            Set<EyeLidRestingStateLeft>((topLid, bottomLid));
            Set<EyeLidRestingStateRight>((topLid, bottomLid));

            // Hair
            var hairColor = RandomColor();
            Set<HairColor>(hairColor);

            var hairStyles = BaseAppearance.GetConstPaths<HairStyle>();
            if (hairStyles.Count > 0)
                Set<HairStyle>(RandomFromList(hairStyles));

            Set<Gender>(RandomRange(0f, 1f));
            Set<Height>(RandomRange(0.8f, 1.2f));
            Set<PupilDilation>(RandomRange(0f, 1f));
            Set<SkinColor>(RandomColor());
            Set<Weight>(RandomRange(0f, 1f));

            #endregion

            #region Face Layers

            var faceColor = RandomColor();

            // Required: Eyes and Face
            var eyes = BaseFaceAppearance.GetConstPaths<Eyes>();
            if (eyes.Count > 0)
                WithFaceLayer<Eyes>(RandomFromList(eyes), faceColor);

            var faces = BaseFaceAppearance.GetConstPaths<Face>();
            if (faces.Count > 0)
                WithFaceLayer<Face>(RandomFromList(faces), faceColor);

            // Optional: FacialHair (50% chance)
            if (UnityEngine.Random.value < 0.5f)
            {
                var facialHair = BaseFaceAppearance.GetConstPaths<FacialHair>();
                if (facialHair.Count > 0)
                    WithFaceLayer<FacialHair>(RandomFromList(facialHair), hairColor);
            }

            #endregion

            #region Body Layers

            var bodyTypes = new (Type type, Action<string, Color> apply)[]
            {
                (typeof(Shirts), (path, color) => WithBodyLayer<Shirts>(path, color)),
                (typeof(Pants), (path, color) => WithBodyLayer<Pants>(path, color))
            };
            foreach (var (type, apply) in bodyTypes.OrderBy(_ => Guid.NewGuid()).Take(UnityEngine.Random.Range(1, 3)))
            {
                MethodInfo method = AccessTools.Method(typeof(BaseBodyAppearance), "GetConstPaths").MakeGenericMethod(type);
                List<string> paths = (List<string>)method.Invoke(null, null);
                if (paths?.Count > 0)
                    apply(RandomFromList(paths), RandomColor());
            }

            #endregion

            #region Accessory Layers

            var accessoryLayers = new (Type type, Action<string, Color> apply)[]
            {
                (typeof(Bottom), (path, color) => WithAccessoryLayer<Bottom>(path, color)),
                (typeof(Chest), (path, color) => WithAccessoryLayer<Chest>(path, color)),
                (typeof(Feet), (path, color) => WithAccessoryLayer<Feet>(path, color)),
                (typeof(Hands), (path, color) => WithAccessoryLayer<Hands>(path, color)),
                (typeof(Head), (path, color) => WithAccessoryLayer<Head>(path, color)),
                (typeof(Neck), (path, color) => WithAccessoryLayer<Neck>(path, color)),
                (typeof(Waist), (path, color) => WithAccessoryLayer<Waist>(path, color))
            };
            foreach (var (type, apply) in accessoryLayers.OrderBy(_ => Guid.NewGuid()).Take(UnityEngine.Random.Range(2, 6)))
            {
                MethodInfo method = AccessTools.Method(typeof(BaseAccessoryAppearance), "GetConstPaths").MakeGenericMethod(type);
                List<string> paths = (List<string>)method.Invoke(null, null);
                if (paths?.Count > 0)
                    apply(RandomFromList(paths), RandomColor());
            }

            #endregion
        }

        #endregion

        #region Private Members

        /// <summary>
        /// INTERNAL: Set's default values to <see cref="S1AvatarFramework.AvatarSettings"/>
        /// </summary>
        /// <remarks>This function is grabbed from the client code</remarks>
        /// <param name="avatarSettings">The AvatarSettings to assign defaults to</param>
        private static void ApplyDefaultSettings(S1AvatarFramework.AvatarSettings avatarSettings)
        {
            avatarSettings.SkinColor = new Color32(150, 120, 95, byte.MaxValue);
            avatarSettings.Height = 0.98f;
            avatarSettings.Gender = 0.0f;
            avatarSettings.Weight = 0.4f;
            avatarSettings.EyebrowScale = 1f;
            avatarSettings.EyebrowThickness = 1f;
            avatarSettings.EyebrowRestingHeight = 0.0f;
            avatarSettings.EyebrowRestingAngle = 0.0f;
            avatarSettings.LeftEyeLidColor = new Color32(150, 120, 95, byte.MaxValue);
            avatarSettings.RightEyeLidColor = new Color32(150, 120, 95, byte.MaxValue);
            avatarSettings.LeftEyeRestingState = new S1AvatarFramework.Eye.EyeLidConfiguration
            {
                bottomLidOpen = 0.5f,
                topLidOpen = 0.5f
            };
            avatarSettings.RightEyeRestingState = new S1AvatarFramework.Eye.EyeLidConfiguration
            {
                bottomLidOpen = 0.5f,
                topLidOpen = 0.5f
            };
            avatarSettings.EyeballMaterialIdentifier = "Default";
            avatarSettings.EyeBallTint = Color.white;
            avatarSettings.PupilDilation = 1f;
            avatarSettings.HairPath = string.Empty;
            avatarSettings.HairColor = Color.black;
        }

        /// <summary>
        /// INTERNAL: The custom <see cref="S1AvatarFramework.AvatarSettings"/> instance used for modders
        /// </summary>
        private S1AvatarFramework.AvatarSettings _customAvatarSettings;

        /// <summary>
        /// INTERNAL: Setters for each individual property (blame IL2CPP)
        /// </summary>
        private static readonly Dictionary<Type, Action<NPCAppearance, object>> _setters = new Dictionary<Type, Action<NPCAppearance, object>>
        {
            [typeof(HairStyle)] = (self, value) => self._customAvatarSettings.HairPath = value as string ?? string.Empty,
            [typeof(HairColor)] = (self, value) =>
            {
                self._customAvatarSettings.HairColor = value switch
                {
                    uint hex => hex.ToColor(),
                    Color color => color,
                    _ => self._customAvatarSettings.HairColor
                };
            },
            [typeof(SkinColor)] = (self, value) =>
            {
                self._customAvatarSettings.SkinColor = value switch
                {
                    uint hex => hex.ToColor(),
                    Color color => color,
                    _ => self._customAvatarSettings.SkinColor
                };
            },
            [typeof(EyeBallTint)] = (self, value) =>
            {
                self._customAvatarSettings.EyeBallTint = value switch
                {
                    uint hex => hex.ToColor(),
                    Color color => color,
                    _ => self._customAvatarSettings.EyeBallTint
                };
            },
            [typeof(Gender)] = (self, value) => self._customAvatarSettings.Gender = Convert.ToSingle(value),
            [typeof(Height)] = (self, value) => self._customAvatarSettings.Height = Convert.ToSingle(value),
            [typeof(Weight)] = (self, value) => self._customAvatarSettings.Weight = Convert.ToSingle(value),
            [typeof(PupilDilation)] = (self, value) => self._customAvatarSettings.PupilDilation = Convert.ToSingle(value),
            [typeof(EyebrowRestingAngle)] = (self, value) => self._customAvatarSettings.EyebrowRestingAngle = Convert.ToSingle(value),
            [typeof(EyebrowRestingHeight)] = (self, value) => self._customAvatarSettings.EyebrowRestingHeight = Convert.ToSingle(value),
            [typeof(EyebrowScale)] = (self, value) => self._customAvatarSettings.EyebrowScale = Convert.ToSingle(value),
            [typeof(EyebrowThickness)] = (self, value) => self._customAvatarSettings.EyebrowThickness = Convert.ToSingle(value),
            [typeof(EyeLidRestingStateLeft)] = (self, value) =>
            {
                var items = value.GetValueTupleItems();
                self._customAvatarSettings.LeftEyeRestingState = new S1AvatarFramework.Eye.EyeLidConfiguration
                {
                    topLidOpen = Convert.ToSingle(items![0]),
                    bottomLidOpen = Convert.ToSingle(items![1])
                };
            },
            [typeof(EyeLidRestingStateRight)] = (self, value) =>
            {
                var items = value.GetValueTupleItems();
                self._customAvatarSettings.RightEyeRestingState = new S1AvatarFramework.Eye.EyeLidConfiguration
                {
                    topLidOpen = Convert.ToSingle(items![0]),
                    bottomLidOpen = Convert.ToSingle(items![1])
                };
            }
        };

        /// <summary>
        /// INTERNAL: The max amount of layers for the Face
        /// </summary>
        private const int MaxFaceLayers = 6;

        /// <summary>
        /// INTERNAL: The max amount of layers for the Body
        /// </summary>
        private const int MaxBodyLayers = 6;

        /// <summary>
        /// INTERNAL: The max amount of layers for Accessories
        /// </summary>
        private const int MaxAccessoryLayers = 9;

        #endregion

    }
}

#if IL2CPP
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
#elif MONO
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
#endif

using System.Collections;
using System.IO;
using MelonLoader;
using Object = UnityEngine.Object;
using MelonLoader.Utils;

namespace S1API.PhoneApp
{
    /// <summary>
    /// Base class for defining in-game phone apps. Automatically clones the phone UI,
    /// injects your app, and supports icon customization.
    /// </summary>
    public abstract class PhoneApp
    {
        /// <summary>
        /// Reference to the player object in the scene.
        /// </summary>
        protected GameObject Player;

        /// <summary>
        /// The actual app panel instance in the phone UI.
        /// </summary>
        protected GameObject AppPanel;

        /// <summary>
        /// Whether the app panel was created by this instance.
        /// </summary>
        protected bool AppCreated;

        /// <summary>
        /// Tracks whether the app icon has been injected into the home screen.
        /// </summary>
        protected bool IconModified;

        /// <summary>
        /// Prevents double-initialization.
        /// </summary>
        protected bool InitializationStarted;

        /// <summary>
        /// Unique internal name for the app GameObject.
        /// </summary>
        protected abstract string AppName { get; }

        /// <summary>
        /// Title text displayed at the top of the app UI.
        /// </summary>
        protected abstract string AppTitle { get; }

        /// <summary>
        /// Label shown below the app icon on the phone home screen.
        /// </summary>
        protected abstract string IconLabel { get; }

        /// <summary>
        /// PNG filename for the app icon (must be placed in UserData folder).
        /// </summary>
        protected abstract string IconFileName { get; }

        /// <summary>
        /// Called after the app is created and a UI container is available.
        /// Implement your custom UI here.
        /// </summary>
        /// <param name="container">The GameObject container inside the app panel.</param>
        protected abstract void OnCreated(GameObject container);

        /// <summary>
        /// Begins async setup of the app, including icon and panel creation.
        /// Should only be called once per session.
        /// </summary>
        /// <param name="logger">Logger to report errors and status.</param>
        public void Init(MelonLogger.Instance logger)
        {
            if (!InitializationStarted)
            {
                InitializationStarted = true;
                MelonCoroutines.Start(DelayedInit(logger));
            }
        }

        /// <summary>
        /// Coroutine that delays setup to ensure all UI elements are ready.
        /// </summary>
        private IEnumerator DelayedInit(MelonLogger.Instance logger)
        {
            yield return new WaitForSeconds(5f);

            Player = GameObject.Find("Player_Local");
            if (Player == null)
            {
                logger.Error("Player_Local not found.");
                yield break;
            }

            GameObject appsCanvas = GameObject.Find("Player_Local/CameraContainer/Camera/OverlayCamera/GameplayMenu/Phone/phone/AppsCanvas");
            if (appsCanvas == null)
            {
                logger.Error("AppsCanvas not found.");
                yield break;
            }

            Transform existingApp = appsCanvas.transform.Find(AppName);
            if (existingApp != null)
            {
                AppPanel = existingApp.gameObject;
                SetupExistingAppPanel(AppPanel, logger);
            }
            else
            {
                Transform templateApp = appsCanvas.transform.Find("ProductManagerApp");
                if (templateApp == null)
                {
                    logger.Error("Template ProductManagerApp not found.");
                    yield break;
                }

                AppPanel = Object.Instantiate(templateApp.gameObject, appsCanvas.transform);
                AppPanel.name = AppName;

                Transform containerTransform = AppPanel.transform.Find("Container");
                if (containerTransform != null)
                {
                    GameObject container = containerTransform.gameObject;
                    ClearContainer(container);
                    OnCreated(container);
                }

                AppCreated = true;
            }

            AppPanel.SetActive(false);

            if (!IconModified)
            {
                IconModified = ModifyAppIcon(IconLabel, IconFileName, logger);
                if (IconModified)
                    logger.Msg("Icon modified.");
            }
        }

        /// <summary>
        /// Sets up an existing app panel found in the scene (likely reused from a previous session).
        /// </summary>
        private void SetupExistingAppPanel(GameObject panel, MelonLogger.Instance logger)
        {
            Transform containerTransform = panel.transform.Find("Container");
            if (containerTransform != null)
            {
                GameObject container = containerTransform.gameObject;
                if (container.transform.childCount < 2)
                {
                    ClearContainer(container);
                    // TODO: (@omar-akermi) Looks like a method got relabeled. Need to resolve :(
                    // BuildUI(container);
                }
            }

            AppCreated = true;
        }

        /// <summary>
        /// Destroys all children of the app container to prepare for UI rebuilding.
        /// </summary>
        private void ClearContainer(GameObject container)
        {
            for (int i = container.transform.childCount - 1; i >= 0; i--)
                Object.Destroy(container.transform.GetChild(i).gameObject);
        }

        /// <summary>
        /// Attempts to clone an app icon from the home screen and customize it.
        /// </summary>
        private bool ModifyAppIcon(string labelText, string fileName, MelonLogger.Instance logger)
        {
            GameObject parent = GameObject.Find("Player_Local/CameraContainer/Camera/OverlayCamera/GameplayMenu/Phone/phone/HomeScreen/AppIcons/");
            if (parent == null)
            {
                logger?.Error("AppIcons not found.");
                return false;
            }

            Transform lastIcon = parent.transform.childCount > 0 ? parent.transform.GetChild(parent.transform.childCount - 1) : null;
            if (lastIcon == null)
            {
                logger?.Error("No icon found to clone.");
                return false;
            }

            GameObject iconObj = lastIcon.gameObject;
            iconObj.name = AppName;

            Transform labelTransform = iconObj.transform.Find("Label");
            Text label = labelTransform?.GetComponent<Text>();
            if (label != null) label.text = labelText;

            return ChangeAppIconImage(iconObj, fileName, logger);
        }

        /// <summary>
        /// Loads the icon PNG from disk and applies it to the cloned icon.
        /// </summary>
        private bool ChangeAppIconImage(GameObject iconObj, string filename, MelonLogger.Instance logger)
        {
            Transform imageTransform = iconObj.transform.Find("Mask/Image");
            Image image = imageTransform?.GetComponent<Image>();
            if (image == null)
            {
                logger?.Error("Image component not found in icon.");
                return false;
            }

            string path = Path.Combine(MelonEnvironment.UserDataDirectory, filename);
            if (!File.Exists(path))
            {
                logger?.Error("Icon file not found: " + path);
                return false;
            }

            try
            {
                byte[] bytes = File.ReadAllBytes(path);
                Texture2D tex = new Texture2D(2, 2);

                if (tex.LoadImage(bytes)) // IL2CPP-safe overload
                {
                    image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                    return true;
                }

                Object.Destroy(tex);
            }
            catch (System.Exception e)
            {
                logger?.Error("Failed to load image: " + e.Message);
            }

            return false;
        }
    }
}

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
using S1API.Internal.Abstraction;

namespace S1API.PhoneApp
{
    /// <summary>
    /// Serves as an abstract base class for creating in-game phone applications with customizable functionality and appearance.
    /// </summary>
    /// <remarks>
    /// Implementations of this class enable the creation of bespoke applications that integrate seamlessly into a game's phone system.
    /// Derived classes are required to define key properties and methods, such as application name, icon, and UI behavior.
    /// This class also manages the app's lifecycle, including its initialization and destruction processes.
    /// </remarks>
    public abstract class PhoneApp : Registerable
    {
        protected static readonly MelonLogger.Instance LoggerInstance = new MelonLogger.Instance("PhoneApp");

        /// <summary>
        /// The player object in the scene.
        /// </summary>
        protected GameObject Player;

        /// <summary>
        /// The in-game UI panel representing the app.
        /// </summary>
        protected GameObject AppPanel;

        /// <summary>
        /// Whether the app was successfully created.
        /// </summary>
        protected bool AppCreated;

        /// <summary>
        /// Whether the app icon was already modified.
        /// </summary>
        protected bool IconModified;

        /// <summary>
        /// Unique GameObject name of the app (e.g. "SilkroadApp").
        /// </summary>
        protected abstract string AppName { get; }

        /// <summary>
        /// The title shown at the top of the app UI.
        /// </summary>
        protected abstract string AppTitle { get; }

        /// <summary>
        /// The label shown under the app icon on the home screen.
        /// </summary>
        protected abstract string IconLabel { get; }

        /// <summary>
        /// The PNG file name (in UserData) used for the app icon.
        /// </summary>
        protected abstract string IconFileName { get; }

        /// <summary>
        /// Called when the app's UI should be created inside the container.
        /// </summary>
        /// <param name="container">The container GameObject to build into.</param>
        protected abstract void OnCreatedUI(GameObject container);

        /// <summary>
        /// Called when the app is loaded into the scene (delayed after phone UI is present).
        /// </summary>
        protected override void OnCreated()
        {
            MelonCoroutines.Start(InitApp());
        }

        /// <summary>
        /// Called when the app is unloaded or the scene is reset.
        /// </summary>
        protected override void OnDestroyed()
        {
            if (AppPanel != null)
            {
                Object.Destroy(AppPanel);
                AppPanel = null;
            }

            AppCreated = false;
            IconModified = false;
        }

        /// <summary>
        /// Coroutine that injects the app UI and icon after scene/UI has loaded.
        /// </summary>
        private IEnumerator InitApp()
        {
            yield return new WaitForSeconds(5f);

            Player = GameObject.Find("Player_Local");
            if (Player == null)
            {
                LoggerInstance.Error("Player_Local not found.");
                yield break;
            }

            GameObject appsCanvas = GameObject.Find("Player_Local/CameraContainer/Camera/OverlayCamera/GameplayMenu/Phone/phone/AppsCanvas");
            if (appsCanvas == null)
            {
                LoggerInstance.Error("AppsCanvas not found.");
                yield break;
            }

            Transform existingApp = appsCanvas.transform.Find(AppName);
            if (existingApp != null)
            {
                AppPanel = existingApp.gameObject;
                SetupExistingAppPanel(AppPanel);
            }
            else
            {
                Transform templateApp = appsCanvas.transform.Find("ProductManagerApp");
                if (templateApp == null)
                {
                    LoggerInstance.Error("Template ProductManagerApp not found.");
                    yield break;
                }

                AppPanel = Object.Instantiate(templateApp.gameObject, appsCanvas.transform);
                AppPanel.name = AppName;

                Transform containerTransform = AppPanel.transform.Find("Container");
                if (containerTransform != null)
                {
                    GameObject container = containerTransform.gameObject;
                    ClearContainer(container);
                    OnCreatedUI(container);
                }

                AppCreated = true;
            }

            AppPanel.SetActive(false);

            if (!IconModified)
            {
                IconModified = ModifyAppIcon(IconLabel, IconFileName);
            }
        }

        /// <summary>
        /// Configures the provided GameObject panel to prepare it for use with the app.
        /// </summary>
        /// <param name="panel">The GameObject representing the UI panel of the app.</param>
        private void SetupExistingAppPanel(GameObject panel)
        {
            Transform containerTransform = panel.transform.Find("Container");
            if (containerTransform != null)
            {
                GameObject container = containerTransform.gameObject;
                if (container.transform.childCount < 2)
                {
                    ClearContainer(container);
                    OnCreatedUI(container);
                }
            }

            AppCreated = true;
        }

        private void ClearContainer(GameObject container)
        {
            for (int i = container.transform.childCount - 1; i >= 0; i--)
                Object.Destroy(container.transform.GetChild(i).gameObject);
        }

        /// <summary>
        /// Modifies the application's icon by cloning an existing icon, updating its label,
        /// and setting a new icon image based on the specified parameters.
        /// </summary>
        /// <param name="labelText">The text to be displayed as the label for the modified icon.</param>
        /// <param name="fileName">The file name of the new icon image to apply.</param>
        /// <returns>
        /// A boolean value indicating whether the icon modification was successful.
        /// Returns true if the modification was completed successfully; otherwise, false.
        /// </returns>
        private bool ModifyAppIcon(string labelText, string fileName)
        {
            GameObject parent = GameObject.Find("Player_Local/CameraContainer/Camera/OverlayCamera/GameplayMenu/Phone/phone/HomeScreen/AppIcons/");
            if (parent == null)
            {
                LoggerInstance?.Error("AppIcons not found.");
                return false;
            }

            Transform lastIcon = parent.transform.childCount > 0 ? parent.transform.GetChild(parent.transform.childCount - 1) : null;
            if (lastIcon == null)
            {
                LoggerInstance?.Error("No icon found to clone.");
                return false;
            }

            GameObject iconObj = lastIcon.gameObject;
            iconObj.name = AppName;

            Transform labelTransform = iconObj.transform.Find("Label");
            Text label = labelTransform?.GetComponent<Text>();
            if (label != null) label.text = labelText;

            return ChangeAppIconImage(iconObj, fileName);
        }

        /// <summary>
        /// Updates the image of the app icon with the specified file, if the file exists and is valid.
        /// </summary>
        /// <param name="iconObj">The GameObject representing the app icon.</param>
        /// <param name="filename">The name of the file containing the new icon image.</param>
        /// <returns>Returns true if the app icon image was successfully changed; otherwise, false.</returns>
        private bool ChangeAppIconImage(GameObject iconObj, string filename)
        {
            Transform imageTransform = iconObj.transform.Find("Mask/Image");
            Image image = imageTransform?.GetComponent<Image>();
            if (image == null)
            {
                LoggerInstance?.Error("Image component not found in icon.");
                return false;
            }

            string path = Path.Combine(MelonEnvironment.UserDataDirectory, filename);
            if (!File.Exists(path))
            {
                LoggerInstance?.Error("Icon file not found: " + path);
                return false;
            }

            try
            {
                byte[] bytes = File.ReadAllBytes(path);
                Texture2D tex = new Texture2D(2, 2);
                if (tex.LoadImage(bytes))
                {
                    image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                    return true;
                }
                Object.Destroy(tex);
            }
            catch (System.Exception e)
            {
                LoggerInstance?.Error("Failed to load image: " + e.Message);
            }

            return false;
        }
    }
}

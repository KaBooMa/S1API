using UnityEngine;
using UnityEngine.UI;
using S1API.PhoneApp;

namespace S1API.PhoneApp
{
    /// <summary>
    /// Defines the MyAwesomeApp, a specialized application integrated into an in-game phone system.
    /// </summary>
    /// <remarks>
    /// This class leverages the PhoneApp framework to specify application-specific properties like name, title,
    /// icon label, and icon file name. It also overrides the method for defining the user interface layout upon creation.
    /// </remarks>
    public class MyAwesomeApp : PhoneApp
    {
        protected override string AppName => "MyAwesomeApp";
        protected override string AppTitle => "My Awesome App";
        protected override string IconLabel => "Awesome";
        protected override string IconFileName => "my_icon.png";

        protected override void OnCreatedUI(GameObject container)
        {
            var panel = UIFactory.Panel("MainPanel", container.transform, Color.black);
            UIFactory.Text("HelloText", "📱 Hello!", panel.transform, 22, TextAnchor.MiddleCenter);
        }
    }
}

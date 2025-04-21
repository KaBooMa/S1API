using UnityEngine;
using UnityEngine.UI;
using S1API.PhoneApp;

namespace S1API.PhoneApp
{
    public class MyAwesomeApp : PhoneApp
    {
        protected override string AppName => "MyAwesomeApp";
        protected override string AppTitle => "My Awesome App";
        protected override string IconLabel => "Awesome";
        protected override string IconFileName => "my_icon.png";

        protected override void OnCreated(GameObject container)
        {
            GameObject panel = UIFactory.Panel("MainPanel", container.transform, Color.black);
            UIFactory.Text("HelloText", "Hello from My Awesome App!", panel.transform, 22, TextAnchor.MiddleCenter, FontStyle.Bold);
        }
    }
}

using MelonLoader;
using S1API.PhoneApp;
[assembly: MelonInfo(typeof(S1API.S1API), "S1API", "1.0.0", "KaBooMa")]

namespace S1API
{
    public class S1API : MelonMod
    {
        public override void OnApplicationStart()
        {
            PhoneAppManager.Register(new MyAwesomeApp());
        }
        private bool _isInGame;

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            bool nowInGame = sceneName != null && sceneName.Contains("Main");

            if (!_isInGame && nowInGame)
            {
                MelonLogger.Msg("Game scene loaded. Initializing phone apps...");
                PhoneAppManager.InitAll(LoggerInstance);
            }
        }
    }
}

using System;
using System.IO;
using System.Reflection;
using MelonLoader;

[assembly: MelonInfo(typeof(S1APILoader.S1APILoader), "S1APILoader", "{VERSION_NUMBER}", "KaBooMa")]

namespace S1APILoader
{
    public class S1APILoader : MelonPlugin
    {
        private const string BuildFolderName = "S1API";
            
        public override void OnPreModsLoaded()
        {
            string? pluginsFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (pluginsFolder == null)
            {
	            throw new Exception("Failed to identify plugins folder.");
            }

            string modsFolder = Path.Combine(pluginsFolder, "../Mods");

            string activeBuild = MelonUtils.IsGameIl2Cpp() ? "Il2Cpp" : "Mono";
            string inactiveBuild = !MelonUtils.IsGameIl2Cpp() ? "Il2Cpp" : "Mono";

            MelonLogger.Msg($"Loading S1API for {activeBuild}...");

            string s1APIActiveBuildFile = Path.Combine(modsFolder, $"S1API.{activeBuild}.MelonLoader.dll");
            string s1APIInactiveBuildFile = Path.Combine(modsFolder, $"S1API.{inactiveBuild}.MelonLoader.dll");

            string disabledActiveBuildFile = $"{s1APIActiveBuildFile}.disabled";
            if (File.Exists(disabledActiveBuildFile))
            {
	            File.Move($"{s1APIActiveBuildFile}.disabled", s1APIActiveBuildFile);
            }

            if (File.Exists(s1APIInactiveBuildFile))
            {
	            File.Move(s1APIInactiveBuildFile, $"{s1APIInactiveBuildFile}.disabled");
            }

            MelonLogger.Msg($"Successfully loaded S1API for {activeBuild}!");
        }
    }
}

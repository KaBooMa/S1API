using System;
using System.IO;
using MelonLoader;

[assembly: MelonInfo(typeof(S1APILoader.S1APILoader), "S1APILoader", "1.0.0", "KaBooMa")]

namespace S1APILoader
{
    public class S1APILoader : MelonPlugin
    {
        private const string BuildFolderName = "S1API";
            
        public override void OnPreModsLoaded()
        {
            string? pluginsFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (pluginsFolder == null)
                throw new Exception("Failed to identify plugins folder.");
            
            string gameFolder = Path.Combine(pluginsFolder, "..");
            string modsFolder = Path.Combine(gameFolder, "Mods");
            
            string buildsFolder = Path.Combine(pluginsFolder, BuildFolderName);

            string activeBuild = MelonUtils.IsGameIl2Cpp() ? "Il2Cpp" : "Mono";
            MelonLogger.Msg($"Loading S1API for {activeBuild}...");
            
            string s1APIBuildFile = Path.Combine(buildsFolder, $"S1API.{activeBuild}.dll");
            

            string s1APIModFile = Path.Combine(modsFolder, "S1API.dll");
            
            File.Copy(s1APIBuildFile, s1APIModFile, true);
            MelonLogger.Msg($"Successfully loaded S1API for {activeBuild}!");
        }
    }
}
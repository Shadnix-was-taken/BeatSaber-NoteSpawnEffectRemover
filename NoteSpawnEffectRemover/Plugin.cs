using Harmony;
using IPA;
using IPA.Config;
using IPA.Utilities;
using System;
using System.Reflection;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;


namespace NoteSpawnEffectRemover
{
    public class Plugin : IBeatSaberPlugin
    {
        public const string Name = "NoteSpawnEffectRemover";
        public const string Version = "1.0.999";

        internal static bool harmonyPatchesLoaded = false;
        internal static HarmonyInstance harmonyInstance = HarmonyInstance.Create("com.shadnix.BeatSaber.NoteSpawnEffectRemover");

        internal static Ref<PluginConfig> config;
        internal static IConfigProvider configProvider;

        public void Init(IPALogger logger, [Config.Prefer("json")] IConfigProvider cfgProvider)
        {
            Logger.log = logger;
            configProvider = cfgProvider;

            config = cfgProvider.MakeLink<PluginConfig>((p, v) =>
            {
                if (v.Value == null || v.Value.RegenerateConfig)
                    p.Store(v.Value = new PluginConfig() { RegenerateConfig = false });
                config = v;
            });
        }

        public void OnApplicationStart()
        {
            
        }

        public void OnApplicationQuit()
        {
            if (harmonyPatchesLoaded)
            {
                Logger.log.Info("Quitting application - removing Harmony patches...");
                UnloadHarmonyPatches();
            }
        }

        public void OnFixedUpdate()
        {

        }

        public void OnUpdate()
        {

        }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {

        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            // Check for scene MenuCore and GameCore, MenuCore for initializing on start, GameCore for changes to config
            if (scene.name == "MenuCore" || scene.name == "GameCore")
            {
                if (!harmonyPatchesLoaded && config.Value._isModEnabled)
                {
                    Logger.log.Info("Loading Harmony patches...");
                    LoadHarmonyPatches();
                    return;
                }
                if (harmonyPatchesLoaded && !config.Value._isModEnabled)
                {
                    Logger.log.Info("Unloading Harmony patches...");
                    UnloadHarmonyPatches();
                    return;
                }
            }
        }

        public void OnSceneUnloaded(Scene scene)
        {

        }

        internal void LoadHarmonyPatches()
        {
            if (harmonyPatchesLoaded)
            {
                Logger.log.Info("Harmony patches already loaded. Skipping...");
                return;
            }
            try
            {
                harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
                Logger.log.Info("Loaded Harmony patches.");
            }
            catch (Exception e)
            {
                Logger.log.Error("Loading Harmony patches failed. Please check if you have Harmony installed.");
                Logger.log.Error(e.ToString());
            }
            harmonyPatchesLoaded = true;
        }

        internal void UnloadHarmonyPatches()
        {
            if (!harmonyPatchesLoaded)
            {
                Logger.log.Info("Harmony patches not loaded. Skipping...");
                return;
            }
            try
            {
                harmonyInstance.UnpatchAll("com.shadnix.BeatSaber.NoteSpawnEffectRemover");
                Logger.log.Info("Unloaded Harmony patches.");
            }
            catch (Exception e)
            {
                Logger.log.Error("Unloading Harmony patches failed.");
                Logger.log.Error(e.ToString());
            }
            harmonyPatchesLoaded = false;
        }
    }
}

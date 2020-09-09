using BeatSaberMarkupLanguage.Settings;
using HarmonyLib;
using IPA;
using NoteSpawnEffectRemover.UI;
using System;
using System.Reflection;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;


namespace NoteSpawnEffectRemover
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        public const string Name = "NoteSpawnEffectRemover";
        public const string Version = "1.9.0";

        internal static bool harmonyPatchesLoaded = false;
        internal static Harmony harmonyInstance = new Harmony("com.shadnix.BeatSaber.NoteSpawnEffectRemover");

        [Init]
        public void Init(object thisIsNull, IPALogger logger)
        {
            Logger.log = logger;
        }

        [OnStart]
        public void OnStart()
        {
            AddEvents();
        }

        [OnExit]
        public void OnExit()
        {
            // Unload Harmony patches
            if (harmonyPatchesLoaded)
            {
                Logger.log.Info("Quitting application - removing Harmony patches...");
                UnloadHarmonyPatches();
            }
            RemoveEvents();
        }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {
            if (nextScene.name == "MenuViewControllers" && prevScene.name == "EmptyTransition")
            {
                BSMLSettings.instance.AddSettingsMenu("Note Spawn Eff.", "NoteSpawnEffectRemover.UI.NoteSpawnEffectRemoverUI.bsml", NoteSpawnEffectRemoverUI.instance);
            }
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            // Check for scene MenuCore and GameCore, MenuCore for initializing on start, GameCore for changes to config
            if (scene.name == "MenuCore" || scene.name == "GameCore")
            {
                if (!harmonyPatchesLoaded && NoteSpawnEffectRemoverUI.instance._isModEnabled)
                {
                    Logger.log.Info("Loading Harmony patches...");
                    LoadHarmonyPatches();
                }
                if (harmonyPatchesLoaded && !NoteSpawnEffectRemoverUI.instance._isModEnabled)
                {
                    Logger.log.Info("Unloading Harmony patches...");
                    UnloadHarmonyPatches();
                }
            }
        }

        private void AddEvents()
        {
            RemoveEvents();
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void RemoveEvents()
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
            SceneManager.sceneLoaded -= OnSceneLoaded;
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

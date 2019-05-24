using Harmony;
using IPA;
using System;
using System.Reflection;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace NoteSpawnEffectRemover
{
    public class Plugin : IBeatSaberPlugin
    {
        public const string Name = "NoteSpawnEffectRemover";
        public const string Version = "1.0.2";

        internal static bool harmonyPatchesLoaded = false;
        internal static HarmonyInstance harmonyInstance = HarmonyInstance.Create("com.shadnix.BeatSaber.NoteSpawnEffectRemover");

        public void Init(object thisWillBeNull, IPALogger logger)
        {
            Logger.log = logger;
        }

        public void OnApplicationStart()
        {
            
        }

        public void OnApplicationQuit()
        {
            
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
            // Do nothing, if patches are already applied
            if (harmonyPatchesLoaded) { return; }

            // Check if we are loading into the main menu
            if (scene.name != "MenuCore") { return; }

            // Patch game
            try
            {
                Logger.log.Info("Loading Harmony patches...");
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

        public void OnSceneUnloaded(Scene scene)
        {

        }
    }
}

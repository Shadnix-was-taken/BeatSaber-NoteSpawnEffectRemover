using BS_Utils.Utilities;


namespace NoteSpawnEffectRemover
{
    public static class Settings
    {
        internal static Config cfgProvider = new Config("NoteSpawnEffectRemover");
        public static bool _isModEnabled;

        static Settings()
        {
            Load();
        }

        public static void Load()
        {
            _isModEnabled = cfgProvider.GetBool("NoteSpawnEffectRemover", nameof(_isModEnabled), true, true);
        }

        public static void Save()
        {
            cfgProvider.SetBool("NoteSpawnEffectRemover", nameof(_isModEnabled), _isModEnabled);
        }
    }
}

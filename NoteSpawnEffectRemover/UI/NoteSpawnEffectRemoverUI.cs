using BeatSaberMarkupLanguage.Attributes;
using BS_Utils.Utilities;

namespace NoteSpawnEffectRemover.UI
{
    public class NoteSpawnEffectRemoverUI : PersistentSingleton<NoteSpawnEffectRemoverUI>
    {
        private Config config;

        [UIValue("boolEnable")]
        public bool _isModEnabled
        {
            get => config.GetBool("NoteSpawnEffectRemover", nameof(_isModEnabled), true, true);
            set => config.SetBool("NoteSpawnEffectRemover", nameof(_isModEnabled), value);
        }

        public void Awake()
        {
            config = new Config("NoteSpawnEffectRemover");
        }

    }

}

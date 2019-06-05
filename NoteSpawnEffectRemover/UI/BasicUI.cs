using CustomUI.GameplaySettings;


namespace NoteSpawnEffectRemover.UI
{
    class BasicUI
    {
        public static void CreateGameplayOptionsUI()
        {
            ToggleOption isModEnabled = GameplaySettingsUI.CreateToggleOption(GameplaySettingsPanels.PlayerSettingsRight, "Remove spawn effect", hintText: "Removes the note spawn effect (the bright light cone) from notes and bombs.");
            isModEnabled.GetValue = Settings._isModEnabled;
            isModEnabled.OnToggle += (value) => { Settings._isModEnabled = value; };
        }
    }
}

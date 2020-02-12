namespace GameFramework
{
    using d4160.GameFramework;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Localization Settings_SO.asset", menuName = "Game Framework/App Settings Data/Localization")]
    public class LocalizationSettings : DefaultLocalizationSettings<LocalizationSettingsSerializableData>
    {
        public override LocalizationSettingsSerializableData Get()
        {
            var data = new LocalizationSettingsSerializableData();
            data.textLanguage = m_textLanguage;
            data.voiceLanguage = m_voiceLanguage;

            return data;
        }

        public override void Set(LocalizationSettingsSerializableData data)
        {
            TextLanguage = data.textLanguage;
            VoiceLanguage = data.voiceLanguage;
        }
    }
}
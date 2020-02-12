namespace GameFramework
{
    using d4160.GameFramework;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Accesibility Settings_SO.asset", menuName = "Game Framework/App Settings Data/Accesibility")]
    public class AccesibilitySettings : DefaultAccesibilitySettings<AccesibilitySettingsSerializableData>
    {
        public override AccesibilitySettingsSerializableData Get()
        {
            var data = new AccesibilitySettingsSerializableData();
            data.uiScale = m_uiScale;
            data.subtitles = m_subtitles;

            return data;
        }

        public override void Set(AccesibilitySettingsSerializableData data)
        {
            UIScale = data.uiScale;
            Subtitles = data.subtitles;
        }
    }
}
namespace GameFramework
{
    using d4160.GameFramework;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New AppStatsSettings_SO.asset", menuName = "Game Framework/App Settings Data/AppStats")]
    public class AppStatsSettings : DefaultAppStatsSettings<AppStatsSettingsSerializableData>
    {
        public override AppStatsSettingsSerializableData Get()
        {
            var data = new AppStatsSettingsSerializableData();
            data.fps = m_fps;
            data.ram = m_ram;
            data.audio = m_audio;
            data.advancedInfo = m_advancedInfo;

            return data;
        }

        public override void Set(AppStatsSettingsSerializableData data)
        {
            Fps = data.fps;
            Ram = data.ram;
            Audio = data.audio;
            AdvancedInfo = data.advancedInfo;
        }
    }
}
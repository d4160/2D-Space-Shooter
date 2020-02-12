namespace GameFramework
{
    using d4160.GameFramework;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New GraphicsSettings_SO.asset", menuName = "Game Framework/App Settings Data/Graphics")]
    public class GraphicsSettings : DefaultGraphicsSettings<GraphicsSettingsSerializableData>
    {
        public override GraphicsSettingsSerializableData Get()
        {
            var data = new GraphicsSettingsSerializableData();
            data.fullScreenMode = m_fullScreenMode;
            data.qualityLevel = m_qualityLevel;
            data.resolution = m_resolution;
            data.vSyncCount = m_vSyncCount;

            return data;
        }

        public override void Set(GraphicsSettingsSerializableData data)
        {
            FullScreenMode = data.fullScreenMode;
            QualityLevel = data.qualityLevel;
            Resolution = data.resolution;
            VSyncCount = data.vSyncCount;
        }
    }
}
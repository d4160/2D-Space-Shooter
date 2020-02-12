namespace GameFramework
{
    using d4160.GameFramework;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New PostProcessingSettings_SO.asset", menuName = "Game Framework/App Settings Data/PostProcessing")]
    public class PostProcessingSettings : DefaultPostProcessingSettings<PostProcessingSettingsSerializableData>
    {
        public override PostProcessingSettingsSerializableData Get()
        {
            var data = new PostProcessingSettingsSerializableData();
            data.bloom = m_bloom;
            data.colorGrading = m_colorGrading;
            data.vignette = m_vignette;

            return data;
        }

        public override void Set(PostProcessingSettingsSerializableData data)
        {
            Bloom = data.bloom;
            ColorGrading = data.colorGrading;
            Vignette = data.vignette;
        }
    }
}
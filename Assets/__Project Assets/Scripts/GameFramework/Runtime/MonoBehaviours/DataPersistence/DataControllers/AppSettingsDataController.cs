namespace GameFramework
{
    using d4160.GameFramework;
    using UnityEngine;

    public class AppSettingsDataController : DataControllerBase
    {
        public override T GetScriptable<T>(int dataIdx = 0)
        {
            var database = GameFrameworkSettings.AppSettingsDatabase;
            var scriptable = database.AppSettingsData[dataIdx];

            return scriptable as T;
        }

        public AppStatsSettings GetAppStatsSettings()
        {
            var scriptable = GetScriptable<AppStatsSettings>(0);
            return scriptable;
        }

        public AudioSettings GetAudioSettings()
        {
            var scriptable = GetScriptable<AudioSettings>(1);
            return scriptable;
        }

        public GraphicsSettings GetGraphicsSettings()
        {
            var scriptable = GetScriptable<GraphicsSettings>(2);
            return scriptable;
        }

        public LocalizationSettings GetLocalizationSettings()
        {
            var scriptable = GetScriptable<LocalizationSettings>(3);
            return scriptable;
        }

        public PostProcessingSettings GetPostProcessingSettings()
        {
            var scriptable = GetScriptable<PostProcessingSettings>(4);
            return scriptable;
        }
    }
}
namespace GameFramework
{
    using d4160.GameFramework;
    using d4160.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    public class AppSettingsDataSerializationAdapter : AppSettingsDataSerializationAdapter<DefaultAppSettingsSerializableData, ConcreteAppSettingsSerializableData>
    {
        public AppSettingsDataSerializationAdapter(DataSerializationAdapterType dataType = DataSerializationAdapterType.Generic) : base(dataType)
        {
        }

        protected override void FillFromSerializableDataForConcreteType(ConcreteAppSettingsSerializableData data)
        {
            if (data == null)
            {
                Debug.LogWarning($"ConcreteAppSettingsSerializableData is null. ");
                return;
            }

            var settings = GameFrameworkSettings.AppSettingsDatabase.AppSettingsData;

            for (int i = 0; i < settings.Length; i++)
            {
                switch(i)
                {
                    case 0: settings[i].FillFromSerializableData(data.AppStatsSettingsData); break;
                    case 1: settings[i].FillFromSerializableData(data.AudioSettingsData); break;
                    case 2: settings[i].FillFromSerializableData(data.GraphicsSettingsData); break;
                    case 3: settings[i].FillFromSerializableData(data.LocalizationSettingsData); break;
                    case 4: settings[i].FillFromSerializableData(data.PostProcessingSettingsData); break;
                }
            }
        }

        protected override ISerializableData GetSerializableDataForConcreteType(ConcreteAppSettingsSerializableData data)
        {
            var settings = GameFrameworkSettings.AppSettingsDatabase.AppSettingsData;

            for (int i = 0; i < settings.Length; i++)
            {
                switch(i)
                {
                    case 0: data.AppStatsSettingsData = (settings[i].GetSerializableData() as AppStatsSettingsSerializableData); break;
                    case 1: data.AudioSettingsData = (settings[i].GetSerializableData() as AudioSettingsSerializableData); break;
                    case 2: data.GraphicsSettingsData = (settings[i].GetSerializableData() as GraphicsSettingsSerializableData); break;
                    case 3: data.LocalizationSettingsData = (settings[i].GetSerializableData() as LocalizationSettingsSerializableData); break;
                    case 4: data.PostProcessingSettingsData = (settings[i].GetSerializableData() as PostProcessingSettingsSerializableData); break;
                }
            }

            return data;
        }
    }
}
namespace GameFramework
{
    using d4160.GameFramework;
    using d4160.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    public class ConcreteAppSettingsSerializableData : ISerializableData, IStorageHelper
    {
        [SerializeField] protected AppStatsSettingsSerializableData m_appStatsSettingsData;
        [SerializeField] protected AudioSettingsSerializableData m_audioSettingsData;
        [SerializeField] protected GraphicsSettingsSerializableData m_graphicsSettingsData;
        [SerializeField] protected LocalizationSettingsSerializableData m_localizationSettingsData;
        [SerializeField] protected PostProcessingSettingsSerializableData m_postProcessingSettingsData;

        public AppStatsSettingsSerializableData AppStatsSettingsData { get => m_appStatsSettingsData; set => m_appStatsSettingsData = value; }
        public AudioSettingsSerializableData AudioSettingsData { get => m_audioSettingsData; set => m_audioSettingsData = value; }
        public GraphicsSettingsSerializableData GraphicsSettingsData { get => m_graphicsSettingsData; set => m_graphicsSettingsData = value; }
        public LocalizationSettingsSerializableData LocalizationSettingsData { get => m_localizationSettingsData; set => m_localizationSettingsData = value; }
        public PostProcessingSettingsSerializableData PostProcessingSettingsData { get => m_postProcessingSettingsData; set => m_postProcessingSettingsData = value; }

        protected StorageHelperType m_storageHelperType;
        protected int m_completedCount;

        public StorageHelperType StorageHelperType
        {
            get => m_storageHelperType;
            set
            {
                m_storageHelperType = value;
                m_appStatsSettingsData.StorageHelperType = value;
                m_audioSettingsData.StorageHelperType = value;
                m_graphicsSettingsData.StorageHelperType = value;
                m_localizationSettingsData.StorageHelperType = value;
                m_postProcessingSettingsData.StorageHelperType = value;
            }
        }

        /// <summary>
        /// custom constructor for serialization purpose.
        /// </summary>
        public ConcreteAppSettingsSerializableData()
        {
        }

        public virtual void Save(bool encrypted = false, System.Action onCompleted = null)
        {
            m_completedCount = 0;

            m_appStatsSettingsData.Save(encrypted, () => OnCompleted(onCompleted));
            m_audioSettingsData.Save(encrypted, () => OnCompleted(onCompleted));
            m_graphicsSettingsData.Save(encrypted, () => OnCompleted(onCompleted));
            m_localizationSettingsData.Save(encrypted, () => OnCompleted(onCompleted));
            m_postProcessingSettingsData.Save(encrypted, () => OnCompleted(onCompleted));
        }

        public virtual void Load(bool encrypted = false, System.Action onCompleted = null)
        {
            m_completedCount = 0;

            m_appStatsSettingsData.Load(encrypted, () => OnCompleted(onCompleted));
            m_audioSettingsData.Load(encrypted, () => OnCompleted(onCompleted));
            m_graphicsSettingsData.Load(encrypted, () => OnCompleted(onCompleted));
            m_localizationSettingsData.Load(encrypted, () => OnCompleted(onCompleted));
            m_postProcessingSettingsData.Load(encrypted, () => OnCompleted(onCompleted));
        }

        protected virtual void OnCompleted(System.Action onCompleted)
        {
            m_completedCount++;

            if (m_completedCount >= 5)
            {
                onCompleted?.Invoke();
            }
        }
    }
}
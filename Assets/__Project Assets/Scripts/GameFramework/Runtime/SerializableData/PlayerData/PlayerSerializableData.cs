namespace GameFramework
{
    using d4160.GameFramework;
    using d4160.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [System.Serializable]
    public class ConcretePlayerSerializableData : ISerializableData, IStorageHelper
    {
        [SerializeField] protected PlayTrialsSerializableData m_playTrialsData;

        protected StorageHelperType m_storageHelperType;
        protected int m_completedCount;

        public PlayTrialsSerializableData PlayTrialsData { get => m_playTrialsData; set => m_playTrialsData = value; }
        public StorageHelperType StorageHelperType
        {
            get => m_storageHelperType;
            set
            {
                m_storageHelperType = value;
                m_playTrialsData.StorageHelperType = value;
            }
        }

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public ConcretePlayerSerializableData()
        {

        }

        public virtual void Save(bool encrypted = false, System.Action onCompleted = null)
        {
            m_completedCount = 0;

            m_playTrialsData.Save(encrypted, () => OnCompleted(onCompleted));
        }

        public virtual void Load(bool encrypted = false, System.Action onCompleted = null)
        {
            m_completedCount = 0;

            m_playTrialsData.Load(encrypted, () => OnCompleted(onCompleted));
        }

        protected virtual void OnCompleted(System.Action onCompleted)
        {
            m_completedCount++;

            if (m_completedCount >= 1)
            {
                onCompleted?.Invoke();
            }
        }
    }
}
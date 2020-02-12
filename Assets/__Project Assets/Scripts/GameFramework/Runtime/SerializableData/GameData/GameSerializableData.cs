namespace GameFramework
{
    using d4160.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [System.Serializable]
    public class ConcreteGameSerializableData : ISerializableData, IStorageHelper
    {
        [SerializeField] protected PlayersSerializableData m_playersData;
        [SerializeField] protected LeaderboardsSerializableData m_leaderboardsData;

        protected StorageHelperType m_storageHelperType;
        protected int m_completedCount;

        public PlayersSerializableData PlayersData { get => m_playersData; set => m_playersData = value; }
        public LeaderboardsSerializableData LeaderboardsData { get => m_leaderboardsData; set => m_leaderboardsData = value; }

        public StorageHelperType StorageHelperType
        {
            get => m_storageHelperType;
            set
            {
                m_storageHelperType = value;
                m_playersData.StorageHelperType = value;
                m_leaderboardsData.StorageHelperType = value;
            }
        }

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public ConcreteGameSerializableData()
        {

        }

        public virtual void Save(bool encrypted = false, System.Action onCompleted = null)
        {
            m_completedCount = 0;

            m_playersData.Save(encrypted, () => OnCompleted(onCompleted));
            m_leaderboardsData.Save(encrypted, () => OnCompleted(onCompleted));
        }

        public virtual void Load(bool encrypted = false, System.Action onCompleted = null)
        {
            m_completedCount = 0;

            m_playersData.Load(encrypted, () => OnCompleted(onCompleted));
            m_leaderboardsData.Load(encrypted, () => OnCompleted(onCompleted));
        }

        protected virtual void OnCompleted(System.Action onCompleted)
        {
            m_completedCount++;

            if (m_completedCount >= 2)
            {
                onCompleted?.Invoke();
            }
        }
    }
}
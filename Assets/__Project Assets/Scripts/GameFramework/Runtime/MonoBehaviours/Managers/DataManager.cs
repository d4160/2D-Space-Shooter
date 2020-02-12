namespace GameFramework
{
    using UnityEngine;
    using d4160.GameFramework;
    using UnityExtensions;

    public class DataManager : DataManagerBase<DataManager>
    {
        [SerializeField, InspectInline] protected GameDataController m_gameData;
        [SerializeField, InspectInline] protected PlayerDataController m_playerData;
        [SerializeField, InspectInline] protected GameFoundationDataController m_gameFoundationData;
        [SerializeField, InspectInline] protected AppSettingsDataController m_appSettingsData;
        [SerializeField, InspectInline] protected LeaderboardController m_leaderboardController;

        public GameDataController GameData => m_gameData;
        public PlayerDataController PlayerData => m_playerData;
        public GameFoundationDataController GameFoundationData => m_gameFoundationData;
        public AppSettingsDataController AppSettingsData => m_appSettingsData;
        public LeaderboardController LeaderboardController => m_leaderboardController;
    }
}
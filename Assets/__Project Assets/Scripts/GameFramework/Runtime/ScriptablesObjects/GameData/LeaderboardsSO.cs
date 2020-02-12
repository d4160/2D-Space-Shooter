namespace GameFramework
{
    using d4160.GameFramework;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New Leaderboards_SO.asset", menuName = "Game Framework/Game Data/Leaderboards")]
    public class LeaderboardsSO : DefaultLeaderboardsSO<DefaultLeaderboardsReorderableArray, DefaultLeaderboard, LeaderboardsSerializableData>
    {
        public override void Set(LeaderboardsSerializableData data)
        {
            m_elements.CopyFrom(data.leaderboards);
        }

        public override LeaderboardsSerializableData Get()
        {
            var data = new LeaderboardsSerializableData(m_elements.ToArray());
            return data;
        }
    }
}
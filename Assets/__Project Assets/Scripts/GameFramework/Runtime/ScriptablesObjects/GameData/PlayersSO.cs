namespace GameFramework
{
    using d4160.GameFramework;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New Players_SO.asset", menuName = "Game Framework/Game Data/Players")]
    public class PlayersSO : DefaultPlayersSO<DefaultPlayersReorderableArray, DefaultPlayer, PlayersSerializableData>
    {
        public override void Set(PlayersSerializableData data)
        {
            m_elements.CopyFrom(data.players);
        }

        public override PlayersSerializableData Get()
        {
            var data = new PlayersSerializableData(m_elements.ToArray());
            return data;
        }
    }
}
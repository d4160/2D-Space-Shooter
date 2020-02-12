namespace GameFramework
{
    using d4160.GameFramework;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New PlayTrials_SO.asset", menuName = "Game Framework/Player Data/PlayTrials")]
    public class PlayTrialsSO : DefaultPlayTrialsSO<DefaultPlayTrialsReorderableArray, DefaultPlayTrial, PlayTrialsSerializableData>
    {
        public override void Set(PlayTrialsSerializableData data)
        {
            m_elements.CopyFrom(data.playTrials);
        }

        public override PlayTrialsSerializableData Get()
        {
            var data = new PlayTrialsSerializableData(m_elements.ToArray());
            return data;
        }
    }
}
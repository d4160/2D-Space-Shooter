namespace GameFramework
{
    using d4160.GameFramework;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New Worlds_SO.asset", menuName = "Game Framework/Game Data/Worlds")]
    public class WorldsSO : DefaultWorldsSO<DefaultWorldsReorderableArray, DefaultWorld, DefaultWorldsSerializableData>
    {
        public override void Set(DefaultWorldsSerializableData data)
        {
        }

        public override DefaultWorldsSerializableData Get()
        {
            return null;
        }
    }
}
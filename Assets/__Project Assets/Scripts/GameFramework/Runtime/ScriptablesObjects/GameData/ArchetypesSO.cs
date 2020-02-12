namespace GameFramework
{
    using d4160.GameFramework;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New Archetypes_SO.asset", menuName = "Game Framework/Game Data/Archetypes")]
    public class ArchetypesSO : DefaultArchetypesSO<DefaultArchetypesReorderableArray, DefaultArchetype, DefaultArchetypesSerializableData>
    {
        public override void Set(DefaultArchetypesSerializableData data)
        {
        }

        public override DefaultArchetypesSerializableData Get()
        {
            return null;
        }
    }
}
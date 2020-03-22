using d4160.GameFramework;
using Unity.Entities;

namespace GameFramework
{
    using UnityEngine;

    public class EntityAuthoring : DefaultEntityAuthoring
    {
        [SerializeField] protected ArchetypeEnum _entity;

        public override int Entity => (int)_entity;

        public override void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            if (enabled)
            {
                dstManager.AddComponentData(entity, new EntityData() { entity = (int)_entity });
            }
        }
    }

}
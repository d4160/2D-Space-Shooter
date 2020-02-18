using Unity.Entities;
using UnityEngine;
using System;

[Flags]
public enum TriggerVolumeType 
{
    None = 0,
    Enemy = 1<<0
}

public struct EnemyOverlappingTriggerVolume : IComponentData { }

public class TriggerVolumeAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public TriggerVolumeType Type = TriggerVolumeType.None;

    void OnEnable() { }

    void IConvertGameObjectToEntity.Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        if (enabled)
        {
            dstManager.AddComponentData(entity, new TriggerVolume()
            {
                Type = (int)Type,
                CurrentFrame = 0,
            });
        }
    }
}
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using Unity.Burst;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Flags]
public enum TriggerVolumeType 
{
    None = 0,
    Enemy = 1<<0
}

public struct OverlappingTriggerVolume : IComponentData
{
    public Entity VolumeEntity;
    public int VolumeType;
    public int CreatedFrame;
    public int CurrentFrame;

    public bool HasJustEntered { get { return (CurrentFrame - CreatedFrame) == 0; } }
}

public struct EnemyOverlappingTriggerVolume : IComponentData { }

public struct TriggerVolume : IComponentData
{
    public int Type;
    public int CurrentFrame;
}

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
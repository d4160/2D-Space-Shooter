using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]
public struct MovementData : IComponentData
{
    public float2 direction;
    public float speed;
}
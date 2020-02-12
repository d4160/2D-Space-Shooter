using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]
public struct LimitsData : IComponentData
{
    public float yUpperLimit;
}

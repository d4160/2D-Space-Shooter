using System;
using Unity.Entities;

[Serializable]
public struct Limits : IComponentData
{
    public float yUpperLimit;
}

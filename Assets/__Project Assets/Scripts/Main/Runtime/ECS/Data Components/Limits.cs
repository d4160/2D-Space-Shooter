using System;
using Unity.Entities;

[Serializable]
public struct Limits : IComponentData
{
    public LimitSide side;
    public float yLimit;
}

public enum LimitSide
{
    Lower,
    Upper
}

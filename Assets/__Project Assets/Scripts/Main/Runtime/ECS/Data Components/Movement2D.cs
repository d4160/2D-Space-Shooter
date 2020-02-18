using Unity.Entities;
using Unity.Mathematics;

[System.Serializable]
public struct Movement2D : IComponentData
{
    public float2 direction;
    public float speed;
}
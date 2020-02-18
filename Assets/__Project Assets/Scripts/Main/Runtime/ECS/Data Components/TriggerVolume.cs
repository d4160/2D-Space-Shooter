using Unity.Entities;

public struct TriggerVolume : IComponentData
{
    public int Type;
    public int CurrentFrame;
}
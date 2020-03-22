using Unity.Entities;

[System.Serializable]
public struct Health : IComponentData
{
    public int lives;
    public bool isInvulnerable;
}

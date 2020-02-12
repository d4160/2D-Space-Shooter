using d4160.GameFramework;
using UnityEngine;

public class TalentGraphProcessor : DefaultTalentGraphProcessor
{
    public TalentGraphProcessor(TalentGraph graph) : base(graph)
    {
    }

    public GameObject LaserPrefab { get; set; }
}

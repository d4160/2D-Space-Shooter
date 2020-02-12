using System.Collections;
using System.Collections.Generic;
using d4160.GameFramework;
using GraphProcessor;
using UnityEngine;

[System.Serializable, NodeMenuItem("Talent/Prefab Override")]
public class PrefabOverrideTalentNode : TalentNode
{
    [SerializeField] private GameObject _prefab;

    public GameObject Prefab
    {
        get => _prefab;
        set => _prefab = value;
    }

    protected override void Process()
    {
        TalentGraphProcessor processor = null;
        if (graph is GameFrameworkBaseGraph baseGraph)
        {
            processor = baseGraph.GetProcessor<TalentGraphProcessor>();

            if (_definition)
            {
                switch (_definition.Type)
                {
                    case TalentType.Laser:
                        processor.LaserPrefab = Prefab;
                        break;
                }
            }
        }
    }

    public override void Unprocess()
    {
        TalentGraphProcessor processor = null;
        if (graph is GameFrameworkBaseGraph baseGraph)
        {
            processor = baseGraph.GetProcessor<TalentGraphProcessor>();

            if (_definition)
            {
                switch (_definition.Type)
                {
                    case TalentType.Laser:
                        processor.LaserPrefab = null;
                        break;
                }
            }
        }
    }
}

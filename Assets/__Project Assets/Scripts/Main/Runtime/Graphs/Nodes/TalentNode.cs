using System.Collections;
using System.Collections.Generic;
using d4160.GameFramework;
using GraphProcessor;
using UnityEngine;

public class TalentNode : TalentBaseNode
{
    [SerializeField] protected TalentDefinition _definition;

    public TalentDefinition Definition
    {
        get => _definition;
        set => _definition = value;
    }

    public override string name {
        get
        {
            if (_definition)
            {
                return $"Talent '{_definition.Name}' ({_definition.Type})"; ;
            }

            return  "Talent (PrefabOverride))";;
        }
    }
}

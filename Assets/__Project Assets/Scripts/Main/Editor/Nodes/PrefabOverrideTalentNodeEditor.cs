using System.Collections;
using System.Collections.Generic;
using d4160.Core.Editors.Utilities;
using GraphProcessor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[NodeCustomEditor(typeof(PrefabOverrideTalentNode))]
public class PrefabOverrideTalentNodeView : BaseNodeView
{
    public override void Enable()
    {
        PrefabOverrideTalentNode node = (nodeTarget as PrefabOverrideTalentNode);

        ObjectField definition = UIElementsUtility.ObjectField(node.Definition, "Definition",(newValue) =>
        {
            owner.RegisterCompleteObjectUndo("Updated PrefabOverrideTalentNode Definition");
            node.Definition = newValue;
        });

        ObjectField prefabField = UIElementsUtility.ObjectField(node.Prefab, "Prefab", (newValue) =>
        {
            owner.RegisterCompleteObjectUndo("Updated PrefabOverrideTalentNode Prefab");
            node.Prefab = newValue;
        });

        Toggle activedField = UIElementsUtility.Toggle(node.Actived, "Actived?", (newValue) =>
        {
            owner.RegisterCompleteObjectUndo("Updated PrefabOverrideTalentNode Actived");
            node.Actived = newValue;
        });

        contentContainer.Add(definition);
        contentContainer.Add(prefabField);
        contentContainer.Add(activedField);
    }
}
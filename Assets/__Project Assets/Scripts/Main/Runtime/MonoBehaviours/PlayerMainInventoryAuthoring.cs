using System.Collections;
using System.Collections.Generic;
using d4160.GameFoundation;
using UnityEngine;
using UnityEngine.GameFoundation;

public class PlayerMainInventoryAuthoring : InventoryItemEvents
{
    protected override void OnItemAdded(InventoryItem item)
    {
        switch (item.displayName)
        {
            case "TripleShot PowerUp":
                var prefabAssets = item.GetDetailDefinition<PrefabAssetsDetailDefinition>();
                if (prefabAssets)
                {
                    var prefab = prefabAssets.GetAsset("Prefab");
                    PlayerLaserFactory.Instance.OverridenPrefab = prefab;
                }

                break;
        }

        base.OnItemAdded(item);
    }

    protected override void OnItemRemoved(InventoryItem item)
    {
        base.OnItemRemoved(item);
    }
}

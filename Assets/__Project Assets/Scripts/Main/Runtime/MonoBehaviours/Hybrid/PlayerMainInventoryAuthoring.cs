using System;
using System.Collections;
using System.Collections.Generic;
using d4160.GameFoundation;
using UnityEngine;
using UnityEngine.GameFoundation;

[RequireComponent(typeof(MultipleTimerCalculator))]
public class PlayerMainInventoryAuthoring : InventoryController
{
    private readonly Dictionary<int, int> _items = new Dictionary<int, int>();
    private MultipleTimerCalculator _timer;

    protected void Awake()
    {
        _timer = GetComponent<MultipleTimerCalculator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _timer.OnTimerOver.DynamicCalls += OnTimerOver;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _timer.OnTimerOver.DynamicCalls -= OnTimerOver;
    }

    private void OnTimerOver(int index)
    {
        foreach (var itemStack in _items)
        {
            if (itemStack.Value == index)
            {
                Inventory.main.RemoveItem(itemStack.Key);
                break;
            }
        }
    }

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

        ProcessItem(item);

        base.OnItemAdded(item);
    }

    protected override void OnItemRemoved(InventoryItem item)
    {
        switch (item.displayName)
        {
            case "TripleShot PowerUp":
                
                PlayerLaserFactory.Instance.OverridenPrefab = null;

                break;
        }

        base.OnItemRemoved(item);
    }

    public void ProcessItem(InventoryItem item)
    {
        if (!_items.ContainsKey(item.hash))
        {
            var index = _timer.AddStat();
            _items.Add(item.hash, index);
        }
    }
}

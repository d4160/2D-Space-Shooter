using System;
using System.Collections;
using System.Collections.Generic;
using d4160.GameFoundation;
using UnityEngine;
using UnityEngine.GameFoundation;

[RequireComponent(typeof(MultipleTimerCalculator), typeof(HealthAuthoring))]
[RequireComponent(typeof(MovementAuthoring), typeof(EquipmentController))]
public class PlayerMainInventoryAuthoring : InventoryController
{
    private readonly Dictionary<int, int> _items = new Dictionary<int, int>();

    private MultipleTimerCalculator _timer;
    private MovementAuthoring _mov;
    private HealthAuthoring _health;
    private EquipmentController _equipment;

    private float _previousSpeed;
    private bool _speedModified;
    private GameObject _shieldInstance;

    protected void Awake()
    {
        _timer = GetComponent<MultipleTimerCalculator>();
        _mov = GetComponent<MovementAuthoring>();
        _health = GetComponent<HealthAuthoring>();
        _equipment = GetComponent<EquipmentController>();
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
                    //var prefab = prefabAssets.GetAsset("Prefab");
                    SingleplayerModeManager.Instance.As<SingleplayerModeManager>().PlayerLaserSpawnProvider
                        .SelectedSourceIndex = 1;
                }

                AddOrSetTimer(item);
                break;

            case "Speed PowerUp":
                if (_speedModified) return;
                
                var modifier = item.GetStatFloat("speed");
                _previousSpeed = _mov.Speed;
                _mov.Speed *= modifier;

                _speedModified = true;

                AddOrSetTimer(item);
                break;

            case "Shield PowerUp":
                if (!_shieldInstance)
                {
                    prefabAssets = item.GetDetailDefinition<PrefabAssetsDetailDefinition>();
                    if (prefabAssets)
                    {
                        var prefab = prefabAssets.GetAsset("Prefab");

                        _shieldInstance = Instantiate(prefab);
                    }
                }

                _equipment.Equip(_shieldInstance);

                _health.SetInvulnerable(true, item.hash);

                break;
        }

        base.OnItemAdded(item);
    }

    protected override void OnItemRemoved(InventoryItem item)
    {
        switch (item.displayName)
        {
            case "TripleShot PowerUp":
                SingleplayerModeManager.Instance.As<SingleplayerModeManager>().PlayerLaserSpawnProvider
                    .SelectedSourceIndex = 0;
                break;

            case "Speed PowerUp":
                if (!_speedModified) return;

                _mov.Speed = _previousSpeed;
                _speedModified = false;
                break;

            case "Shield PowerUp":
                _equipment.Unequip();
                _health.SetInvulnerable(false);
                break;
        }

        base.OnItemRemoved(item);
    }

    public void AddOrSetTimer(InventoryItem item)
    {
        if (!_items.ContainsKey(item.hash))
        {
            var index = _timer.AddNewStat(0);
            _items.Add(item.hash, index);
        }
        else
        {
            _timer.CalculateStat(_items[item.hash]);
        }
    }
}

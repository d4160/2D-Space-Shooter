using System.Collections;
using System.Collections.Generic;
using d4160.Core;
using d4160.GameFramework;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class PlayerLaserSpawnProvider : HybridSpawnProvider
{
    [Header("SPAWN OPTIONS")]
    [SerializeField] private int _fireBySeconds = 7;

    private float _timeBetweenFires;
    private float _nextFireTime;

    public override bool CanSpawn => Time.time >= _nextFireTime;
    public override Vector3 SpawnPosition {
        get
        {
            Vector3 desiredPos = SingleplayerModeManager.Instance.As<SingleplayerModeManager>().PlayerPosition;
            desiredPos += _offset;

            return desiredPos;
        }
    }

    private void Start()
    {
        _timeBetweenFires = 1f / _fireBySeconds;
    }

    protected override void EntitySpawn(Entity instance)
    {
        base.EntitySpawn(instance);

        _nextFireTime = Time.time + _timeBetweenFires;
    }

    protected override void GameObjectSpawn(GameObject instance)
    {
        base.GameObjectSpawn(instance);

        _nextFireTime = Time.time + _timeBetweenFires;
    }
}

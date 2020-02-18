using System.Collections;
using System.Collections.Generic;
using d4160.Core;
using d4160.GameFramework;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class PlayerLaserFactory : EntityHybridFactoryBase<PlayerLaserFactory>
{
    [Header("SPAWN OPTIONS")]
    [SerializeField] private int _fireBySeconds = 3;
    [SerializeField] private Vector2 _instanceOffset = new Vector3(0f, .8f);
    [Range(0f, 31f)]
    [SerializeField] private float _overrideSpeed = 0f;

    private float _timeBetweenFires;
    private float _nextFireTime;
    private GameObject _overridenPrefab;

    private bool CanFire => Time.time >= _nextFireTime;

    public GameObject OverridenPrefab
    {
        get => _overridenPrefab;
        set => _overridenPrefab = value;
    }
    public override GameObject Prefab => _overridenPrefab ? _overridenPrefab : _prefab;

    private void Start()
    {
        _timeBetweenFires = 1f / _fireBySeconds;
    }

    public void Fire()
    {
        if (!CanFire) return;
        
        SelectableFactoryProcedure();
    }

    public override void EntityFactoryProcedure()
    {
        var desiredPos = SingleplayerModeManager.Instance.As<SingleplayerModeManager>().PlayerPosition;
        desiredPos += _instanceOffset;

        var newLaser = Fabricate();
        var position = new float3(desiredPos.x, desiredPos.y, 0f);
        _entityManager.SetComponentData(newLaser, new Translation { Value = position });

        if (_overrideSpeed > 0f)
        {
            _entityManager.SetComponentData(newLaser, new Movement2D
            {
                direction = new float2(0f, 1f),
                speed = _overrideSpeed
            });
        }

        _nextFireTime = Time.time + _timeBetweenFires;
    }

    public override void GameObjectFactoryProcedure()
    {
        var desiredPos = SingleplayerModeManager.Instance.As<SingleplayerModeManager>().PlayerPosition;
        desiredPos += _instanceOffset;

        var newLaser = FabricateSecond();
        newLaser.transform.position = desiredPos;

        if (_overrideSpeed > 0f)
        {
            newLaser.GetComponent<Laser>().Speed = _overrideSpeed;
        }

        _nextFireTime = Time.time + _timeBetweenFires;
    }
}

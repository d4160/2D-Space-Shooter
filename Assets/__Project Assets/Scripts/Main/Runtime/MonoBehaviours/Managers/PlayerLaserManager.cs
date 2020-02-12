using System.Collections;
using System.Collections.Generic;
using d4160.Core;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class PlayerLaserManager : Singleton<PlayerLaserManager>, IFactory<Entity>
{
    public bool useECS = false;

    [Header("SPAWN OPTIONS")]
    [SerializeField] protected GameObject _laserPrefab;
    [SerializeField] private int _fireBySeconds = 3;
    [SerializeField] private Vector2 _instanceOffset = new Vector3(0f, .8f);
    [Range(0f, 31f)]
    [SerializeField] private float _overrideSpeed = 0f;

    private float _timeBetweenFires;
    private float _nextFireTime;
    private EntityManager _entityManager;
    private Entity _laserEntityPrefab;

    private bool CanFire => Time.time >= _nextFireTime;

    void Start()
    {
        if (!useECS) return;

        _timeBetweenFires = 1f / _fireBySeconds;

        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        GameObjectConversionSettings settings =
            GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, new BlobAssetStore());
        
        _laserEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(_laserPrefab, settings);
    }

    public void Fire()
    {
        if (!useECS) return;

        if (!CanFire) return;
        ;
        var desiredPos = SingleplayerModeManager.Instance.As<SingleplayerModeManager>().PlayerPosition;
        desiredPos += _instanceOffset;

        var newLaserEntity = _entityManager.Instantiate(_laserEntityPrefab);
        var position = new float3(desiredPos.x, desiredPos.y, 0f);
        _entityManager.SetComponentData(newLaserEntity, new Translation {Value = position});

        if (_overrideSpeed > 0f)
        {
            _entityManager.SetComponentData(newLaserEntity, new MovementData
            {
                direction = new float2(0f, 1f),
                speed = _overrideSpeed
            });
        }

        _nextFireTime = Time.time + _timeBetweenFires;
    }

    public Entity Fabricate(int option = 0)
    {
        throw new System.NotImplementedException();
    }
}

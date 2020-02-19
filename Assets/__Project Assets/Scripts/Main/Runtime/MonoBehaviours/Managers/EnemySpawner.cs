using System;
using System.Collections;
using System.Collections.Generic;
using d4160.Core;
using d4160.GameFramework;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class EnemySpawner : EntityHybridFactory<EnemySpawner>
{
    [Header("SPAWN OPTIONS")]
    [SerializeField] private Transform _enemiesParent;
    [Range(0f, 5f)]
    [SerializeField] private float _spawnRate = 1f;
    [Range(1, 10000)]
    [SerializeField] private int _spawnsNumber = 3;
    [SerializeField] private float _overrideSpeed;

    private WaitForSeconds _waitForSpawn;
    private Coroutine _spawnCoroutine;

    private void Start()
    {
        _waitForSpawn = new WaitForSeconds(_spawnRate);

        _spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            for (int i = 0; i < _spawnsNumber; i++)
            {
                Spawn();
            }
            
            if (_spawnRate == 0.0f)
                yield break;

            yield return _waitForSpawn;
        }
    }

    private void Spawn()
    {
        var manager = SingleplayerModeManager.Instance.As<SingleplayerModeManager>();
        var xLimits = manager.EnemyXLimits;
        var yLimits = manager.EnemyYLimits;

        var desiredPosition = new Vector3(xLimits.Random(), yLimits.y, 0f);

        if (_useECS)
        {
            //var newEnemy = _entityManager.Instantiate(_enemyEntityPrefab);
            //_entityManager.SetComponentData(newEnemy, new Translation { Value = desiredPosition});

            //if (_overrideSpeed > 0f)
            //{
            //    _entityManager.SetComponentData(newEnemy, new Movement2D
            //    {
            //        direction = new float2(0f, -1f),
            //        speed = _overrideSpeed
            //    });
            //}
        }
        else
        {
            //var newEnemy = Instantiate(_enemyPrefab, desiredPosition, Quaternion.identity);
            //newEnemy.transform.SetParent(_enemiesParent);

            //if (_overrideSpeed > 0f)
            //    newEnemy.Speed = _overrideSpeed;
        }
    }

    public void StopSpawnRoutine()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }
    }

    public override void EntityFactoryProcedure()
    {
        throw new NotImplementedException();
    }

    public override void GameObjectFactoryProcedure()
    {
        throw new NotImplementedException();
    }
}

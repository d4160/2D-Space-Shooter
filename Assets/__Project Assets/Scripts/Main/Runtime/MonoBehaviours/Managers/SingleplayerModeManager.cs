using System.Collections;
using System.Collections.Generic;
using d4160.GameFoundation;
using d4160.GameFramework;
using GameFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityExtensions;

public class SingleplayerModeManager : GameModeManagerBase
{
    [Header("PLAYER OPTIONS")]
    [SerializeField] protected Vector2 _playerXLimits = new Vector2(-11.3f, 11.3f);
    [SerializeField] protected Vector2 _playerYLimits = new Vector2(-3.8f, 0f);
    [InspectInline(canEditRemoteTarget = true)]
    [SerializeField] protected PlayerLaserSpawnProvider _playerLaserSpawnProvider;
    [SerializeField] protected MultipleStatCalculator _playerStatCalculator;
    [Header("ENEMY OPTIONS")]
    [SerializeField] protected Vector2 _enemyXLimits = new Vector2(-8.5f, 8.5f);
    [SerializeField] protected Vector2 _enemyYLimits = new Vector2(-6.5f, 6.5f);
    [InspectInline(canEditRemoteTarget = true)]
    [SerializeField] protected EnemySpawnProvider _enemySpawnProvider;
    [InspectInline(canEditRemoteTarget = true)]
    [SerializeField] protected HybridSpawnProvider _enemyLaserSpawnProvider;
    [Header("POWER UP OPTIONS")]
    [SerializeField] protected Vector2 _powerUpXLimits = new Vector2(-8.5f, 8.5f);
    [SerializeField] protected Vector2 _powerUpYLimits = new Vector2(-6.5f, 6.5f);
    [InspectInline(canEditRemoteTarget = true)]
    [SerializeField] protected PowerUpSpawnProvider _powerUpSpawnProvider;
    [Header("OTHER")]
    [InspectInline(canEditRemoteTarget = true)]
    [SerializeField] protected InputButtonActions _inputActions;
    [InspectInline(canEditRemoteTarget = true)]
    [SerializeField] protected Spawner[] _spawners;
    [InspectInline(canEditRemoteTarget = true)]
    [SerializeField] protected LevelLoader _levelLoader;

    private Vector2 _playerDirection;

    public Vector2 PlayerDirection => _playerDirection;
    public Vector2 PlayerXLimits => _playerXLimits;
    public Vector2 PlayerYLimits => _playerYLimits;
    public Vector2 PlayerPosition { get; set; }
    public Vector2 EnemyXLimits => _enemyXLimits;
    public Vector2 EnemyYLimits => _enemyYLimits;
    public Vector2 PowerUpXLimits => _powerUpXLimits;
    public Vector2 PowerUpYLimits => _powerUpYLimits;
    public PlayerLaserSpawnProvider PlayerLaserSpawnProvider => _playerLaserSpawnProvider;
    public EnemySpawnProvider EnemySpawnProvider => _enemySpawnProvider;
    public HybridSpawnProvider EnemyLaserSpawnProvider => _enemyLaserSpawnProvider;
    public PowerUpSpawnProvider PowerUpSpawnProvider => _powerUpSpawnProvider;
    public MultipleStatCalculator PlayerStatCalculator => _playerStatCalculator;
    public InputButtonActions InputActions => _inputActions;
    public LevelLoader LevelLoader => _levelLoader;

    public void SetPlayerDirection(Vector2 value)
    {
        _playerDirection = value;
    }

    public void SetPlayerStatCalculator(MultipleStatCalculator value)
    {
        _playerStatCalculator = value;
    }

    public void SetPlayerInputButtonActions(InputButtonActions value)
    {
        _inputActions = value;
    }

    public override void Despawn(GameObject instance, int entity, int poolIndex = 0, float delay = 0f)
    {
        Despawn(instance, (ArchetypeEnum)entity, poolIndex, delay);
    }

    public override void Despawn(GameObject instance, int entity, int category, int poolIndex = 0, float delay = 0f)
    {
        Despawn(instance, (ArchetypeEnum)entity, category, poolIndex, delay);
    }

    public void Despawn(GameObject instance, ArchetypeEnum entity, int poolIndex = 0, float delay = 0f)
    {
        switch (entity)
        {
            case ArchetypeEnum.Laser:
                _playerLaserSpawnProvider.Despawn(instance, poolIndex, delay);
                break;

            case ArchetypeEnum.Enemy:
                _enemySpawnProvider.Despawn(instance, poolIndex, delay);
                break;

            case ArchetypeEnum.PowerUp:
                _powerUpSpawnProvider.Despawn(instance, poolIndex, delay);
                break;
        }
    }

    public void Despawn(GameObject instance, ArchetypeEnum entity, int category, int poolIndex = 0, float delay = 0f)
    {
        switch (entity)
        {
            case ArchetypeEnum.Laser:
                switch (category)
                {
                    case 0:
                        _playerLaserSpawnProvider.Despawn(instance, poolIndex, delay);
                        break;
                    case 1:
                        _enemyLaserSpawnProvider.Despawn(instance, poolIndex, delay);
                        break;
                }
                break;

            case ArchetypeEnum.Enemy:
                _enemySpawnProvider.Despawn(instance, poolIndex, delay);
                break;

            case ArchetypeEnum.PowerUp:
                _powerUpSpawnProvider.Despawn(instance, poolIndex, delay);
                break;
        }
    }

    public override void StartSpawner(int spawnIndex = -1)
    {
        switch (spawnIndex)
        {
            case -1:
                for (int i = 0; i < _spawners.Length; i++)
                {
                    _spawners[i]?.StartSpawn();
                }
                break;
        }
    }

    public override void StopSpawner(int spawnIndex = -1)
    {
        switch (spawnIndex)
        {
            case -1:
                for (int i = 0; i < _spawners.Length; i++)
                {
                    _spawners[i]?.StopSpawn();
                }
                break;
        }
    }
}
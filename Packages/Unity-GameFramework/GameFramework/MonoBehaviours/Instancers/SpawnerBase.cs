using System.Collections;
using System.Collections.Generic;
using d4160.Core;
using Lean.Pool;
using UnityEngine;
using UnityExtensions;

namespace d4160.GameFramework   
{
    public abstract class SpawnerBase : MonoBehaviour
    {
        [Header("OBJECT OPTIONS")]
        [InspectInline(canEditRemoteTarget = true)]
        [SerializeField] protected LeanGameObjectPool _pool;

        [Header("SPAWN OPTIONS")]
        [SerializeField] protected bool _startSpawnAtStart;
        [Range(0, 180f)]
        [SerializeField] protected float _spawnRate;
        [SerializeField] protected Vector2Int _minMaxSpawnsNumber;

        protected WaitForSeconds _waitForSpawn;
        protected Coroutine _spawnCoroutine;

        protected virtual Vector3? NewPosition => null;
        protected virtual Quaternion? NewRotation => null;

        public float SpawnRate
        {
            get => _spawnRate;
            set
            {
                _spawnRate = value;
                SetWaitForSeconds();
            }
        } 

        protected virtual void Start()
        {
            if (_startSpawnAtStart)
            {
                StartSpawn();
            }
        }

        protected virtual void Spawn()
        {
            if (_pool)
            {
                var number = _minMaxSpawnsNumber.Random();

                for (var i = 0; i < number; i++)
                {
                    var go = _pool.Spawn();
                    if (NewPosition.HasValue && NewRotation.HasValue)
                    {
                        go.transform.SetPositionAndRotation(NewPosition.Value, NewRotation.Value);
                    }
                    else if (NewPosition.HasValue)
                    {
                        go.transform.position = NewPosition.Value;
                    }
                    else if (NewRotation.HasValue)
                    {
                        go.transform.rotation = NewRotation.Value;
                    }
                }
            }
        }

        protected virtual IEnumerator SpawnRoutine()
        {
            while (true)
            {
                Spawn();

                if (_spawnRate == 0.0f)
                    yield break;

                yield return _waitForSpawn;
            }
        }

        protected void SetWaitForSeconds()
        {
            _waitForSpawn = new WaitForSeconds(_spawnRate);
        }

        public virtual void StartSpawn()
        {
            if (_spawnCoroutine != null) return;

            if (_waitForSpawn == null)
            {
                if (_spawnRate != 0.0f)
                {
                    SetWaitForSeconds();
                }
            }

            _spawnCoroutine = StartCoroutine(SpawnRoutine());
        }

        public virtual void StopSpawn()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }
        }
    }
}


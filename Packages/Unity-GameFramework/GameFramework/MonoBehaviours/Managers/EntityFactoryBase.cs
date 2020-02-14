using System.Collections;
using System.Collections.Generic;
using d4160.Core;
using Unity.Entities;
using UnityEngine;

namespace d4160.GameFramework
{
    public abstract class EntityFactoryBase<T> : Singleton<T>, IFactory<Entity>, IInitializable where T : MonoBehaviour
    {
        [Header("PREFAB OPTIONS")]
        [SerializeField] protected GameObject _prefab;

        protected EntityManager _entityManager;
        protected GameObjectConversionSettings _conversionSettings;
        protected BlobAssetStore _assetStore;
        protected Entity _entityPrefab;

        public virtual GameObject Prefab => _prefab;

        protected override void Awake()
        {
            base.Awake();

            Initialize();

            ConvertGameObjectPrefab();
        }

        protected virtual void OnDestroy()
        {
            Deinitialize();
        }

        public virtual void Initialize()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _assetStore = new BlobAssetStore();
            _conversionSettings =
                GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, new BlobAssetStore());
        }

        public virtual void Deinitialize()
        {
            _assetStore.Dispose();
        }

        public virtual void ConvertGameObjectPrefab()
        {
            if (Prefab)
            {
                _entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(Prefab, _conversionSettings);
            }
        }

        public virtual Entity Fabricate(int option = 0)
        {
            return _entityManager.Instantiate(_entityPrefab);
        }

        public abstract void EntityFactoryProcedure();
    }
}
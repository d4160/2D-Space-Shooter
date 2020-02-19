using System.Collections;
using System.Collections.Generic;
using d4160.Core;
using UnityEngine;

namespace d4160.GameFramework
{
    public abstract class HybridSpawnerBase : EntityFactory, IFactorySecond<GameObject> where T : MonoBehaviour
    {
        [Header("HYBRID OPTIONS")]
        [SerializeField] protected bool _useECS = false;

        protected override void Awake()
        {
            SetSingletonOnAwake();

            if (_useECS)
            {
                Initialize();

                ConvertGameObjectPrefab();
            }
        }

        public GameObject FabricateSecond(int option = 0)
        {
            return Instantiate(Prefab);
        }

        public abstract void GameObjectFactoryProcedure();

        public virtual void SelectableFactoryProcedure()
        {
            if (_useECS)
            {
                EntityFactoryProcedure();
            }
            else
            {
                GameObjectFactoryProcedure();
            }
        }
    }
}
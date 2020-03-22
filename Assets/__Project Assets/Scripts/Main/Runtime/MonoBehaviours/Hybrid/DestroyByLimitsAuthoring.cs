using System.Collections;
using System.Collections.Generic;
using d4160.GameFramework;
using Lean.Pool;
using UnityEngine;

[RequireComponent(typeof(DefaultDestroyable))]
public class DestroyByLimitsAuthoring : AuthoringBehaviourBase<Limits>
{
    private DefaultDestroyable _destroyable;

    private void Awake()
    {
        _destroyable = GetComponent<DefaultDestroyable>();
    }

    private void Update()
    {
        switch (_data.side)
        {
            case LimitSide.Lower:
                if (transform.position.y < _data.yLimit)
                {
                    _destroyable.Destroy();
                }
                break;
            case LimitSide.Upper:
                if (transform.position.y > _data.yLimit)
                {
                    _destroyable.Destroy();
                }
                break;
        }
    }
}

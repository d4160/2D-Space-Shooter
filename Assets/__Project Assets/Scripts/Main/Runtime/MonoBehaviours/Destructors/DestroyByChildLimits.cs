using System.Collections;
using System.Collections.Generic;
using d4160.GameFramework;
using Lean.Pool;
using UnityEngine;

[RequireComponent(typeof(DefaultDestroyable))]
public class DestroyByChildLimits : AuthoringBehaviourBase<Limits>
{
    private Transform _child;
    private DefaultDestroyable _destroyable;

    public Transform Child
    {
        get
        {
            if (_child && _child.gameObject.activeSelf) return _child;

            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.gameObject.activeSelf)
                {
                    _child = child;
                    break;
                }
            }

            return _child;
        }
    }

    private void Awake()
    {
        _destroyable = GetComponent<DefaultDestroyable>();
    }

    private void Update()
    {
        switch (_data.side)
        {
            case LimitSide.Lower:
                if (Child.position.y < _data.yLimit)
                {
                    _destroyable.Destroy();
                }
                break;
            case LimitSide.Upper:
                if (Child.position.y > _data.yLimit)
                {
                    _destroyable.Destroy();
                }
                break;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using d4160.GameFramework;
using d4160.Loops;
using UnityEngine;

public class RotationAuthoring : AuthoringBehaviourBase<Rotation2D>
{
    protected virtual void OnEnable()
    {
        UpdateLoop.OnUpdate += OnUpdate;
    }

    protected virtual void OnDisable()
    {
        UpdateLoop.OnUpdate -= OnUpdate;
    }

    protected void OnUpdate(float dt)
    {
        transform.Rotate(Vector3.forward * _data.speed * dt);
    }
}

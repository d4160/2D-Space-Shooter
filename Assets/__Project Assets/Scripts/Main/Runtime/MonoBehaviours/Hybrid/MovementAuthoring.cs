using d4160.GameFramework;
using d4160.Loops;
using UnityEngine;

public class MovementAuthoring : AuthoringBehaviourBase<Movement2D>
{
    public float Speed
    {
        get => _data.speed;
        set => _data.speed = value;
    }

    protected virtual void OnEnable()
    {
        UpdateLoop.OnUpdate += OnUpdate;
    }

    protected virtual void OnDisable()
    {
        UpdateLoop.OnUpdate -= OnUpdate;
    }

    protected virtual void OnUpdate(float dt)
    {
        transform.Translate((Vector2)_data.direction * _data.speed * dt);
    }
}

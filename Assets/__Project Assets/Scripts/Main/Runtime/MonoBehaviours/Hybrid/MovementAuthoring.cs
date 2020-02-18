using d4160.GameFramework;
using UnityEngine;

public class MovementAuthoring : AuthoringBehaviourBase<Movement2D>
{
    public float Speed
    {
        get => _data.speed;
        set => _data.speed = value;
    }

    protected virtual void Update()
    {
        transform.Translate((Vector2)_data.direction * _data.speed * Time.deltaTime);
    }
}

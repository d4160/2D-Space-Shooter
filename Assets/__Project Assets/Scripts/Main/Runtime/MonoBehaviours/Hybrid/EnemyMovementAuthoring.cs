using System.Collections;
using System.Collections.Generic;
using d4160.Core;
using UnityEngine;

public class EnemyMovementAuthoring : MovementAuthoring
{
    public bool AllowReturnBack { get; set; } = true;

    protected override void OnUpdate(float dt)
    {
        DoMove(dt);
    }

    private void DoMove(float dt)
    {
        transform.Translate((Vector2)_data.direction * _data.speed * dt);

        if (!SingleplayerModeManager.Instanced || !AllowReturnBack) return;
        
        SingleplayerModeManager manager = SingleplayerModeManager.Instance.As<SingleplayerModeManager>();
        Vector2 xLimits = manager.EnemyXLimits;
        Vector2 yLimits = manager.EnemyYLimits;

        Vector3 newPos = transform.position;
        if (newPos.y < yLimits.x)
        {
            newPos.y = yLimits.y;
            newPos.x = xLimits.Random();
            transform.position = newPos;
        }
    }
}

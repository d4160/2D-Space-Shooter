using System.Collections;
using System.Collections.Generic;
using d4160.Core;
using UnityEngine;

public class EnemyMovementAuthoring : MovementAuthoring
{
    protected override void Update()
    {
        DoMove();
    }

    private void DoMove()
    {
        SingleplayerModeManager manager = SingleplayerModeManager.Instance.As<SingleplayerModeManager>();
        Vector2 xLimits = manager.EnemyXLimits;
        Vector2 yLimits = manager.EnemyYLimits;

        transform.Translate((Vector2)_data.direction * _data.speed * Time.deltaTime);

        Vector3 newPos = transform.position;
        if (newPos.y < yLimits.x)
        {
            newPos.y = yLimits.y;
            newPos.x = xLimits.Random();
            transform.position = newPos;
        }
    }
}

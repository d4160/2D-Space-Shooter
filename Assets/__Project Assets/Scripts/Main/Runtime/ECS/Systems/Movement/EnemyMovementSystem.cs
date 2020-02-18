using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;
using d4160.Core;

[AlwaysSynchronizeSystem]
public class EnemyMovementSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        SingleplayerModeManager manager = SingleplayerModeManager.Instance.As<SingleplayerModeManager>();
        Vector2 xLimits = manager.EnemyXLimits;
        Vector2 yLimits = manager.EnemyYLimits;
        float deltaTime = Time.DeltaTime;

        Entities
            .WithAll<EnemyTag>()
            .ForEach(
                (ref Translation trans, in Movement2D mov) =>
                {
                    float3 nextPos = trans.Value;
                    nextPos.xy += (mov.direction * mov.speed * deltaTime);

                    if (nextPos.y < yLimits.x)
                    {
                        nextPos.y = yLimits.y;
                        nextPos.x = xLimits.Random();
                    }

                    trans.Value = nextPos;
                }
            ).Run();

        return default;
    }
}

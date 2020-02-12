using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class LaserMovementSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;

        Entities
            .WithAll<LaserTag>()
            .ForEach((ref Translation trans, in MovementData mov) =>
                {
                    trans.Value.xy += (mov.direction * mov.speed * deltaTime);
                }).Run();

        return default;
    }
}

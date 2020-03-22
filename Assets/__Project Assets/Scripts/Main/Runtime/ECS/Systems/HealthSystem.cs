using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class HealthSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        bool destroyed = false;

        Entities.ForEach((Entity entity, in Health health) =>
        {
            if (health.lives < 1)
            {
                ecb.DestroyEntity(entity);
                destroyed = true;
            }
        }).Run();

        ecb.Playback(EntityManager);
        ecb.Dispose();

        //if (destroyed)
        //    EnemySpawnProvider.Instance.StopSpawnRoutine();

        return default;
    }
}

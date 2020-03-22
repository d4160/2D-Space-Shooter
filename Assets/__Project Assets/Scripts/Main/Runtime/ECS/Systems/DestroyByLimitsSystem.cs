using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class DestroyByLimitsSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);

        Entities.ForEach((Entity entity, in Limits limits, in Translation trans) =>
        {
            float3 pos = trans.Value;
            switch (limits.side)
            {
                case LimitSide.Lower:
                    if (pos.y < limits.yLimit)
                    {
                        ecb.DestroyEntity(entity);
                    }
                    break;
                case LimitSide.Upper:
                    if (pos.y > limits.yLimit)
                    {
                        ecb.DestroyEntity(entity);
                    }
                    break;
            }
            
        }).Run();

        ecb.Playback(EntityManager);
        ecb.Dispose();

        return default;
    }
}

using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;
using float2 = Unity.Mathematics.float2;

[AlwaysSynchronizeSystem]
public class PlayerMovementSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        SingleplayerModeManager manager = SingleplayerModeManager.Instance.As<SingleplayerModeManager>();
        float2 xLimits = manager.PlayerXLimits;
        float2 yLimits = manager.PlayerYLimits;

        Entities
            .WithAll<PlayerTag>()
            .ForEach(
            (ref Translation trans, 
             in Movement2D mov) =>
            {
                float2 newPos = trans.Value.xy;
                float2 movement = mov.direction * mov.speed * deltaTime;
                
                newPos += movement;
                newPos = CalculateFixedPos(newPos, yLimits, xLimits);

                trans.Value.xy = newPos;
            }
        ).Run();

        return default;
    }

    private static float2 CalculateFixedPos(float2 pos, float2 yLimits, float2 xLimits)
    {
        pos.y = clamp(pos.y, yLimits.x, yLimits.y);
        
        if (pos.x < xLimits.x)
            pos.x = xLimits.y;
        else if (pos.x > xLimits.y)
            pos.x = xLimits.x;

        return pos;
    }
}
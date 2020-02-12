using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class PlayerInputSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        SingleplayerModeManager manager = SingleplayerModeManager.Instance.As<SingleplayerModeManager>();
        Vector2 direction = manager.PlayerDirection;
        Entities
            .WithAll<PlayerTag>()
            .WithoutBurst()
            .ForEach((ref MovementData mov, in Translation trans) =>
            {
                mov.direction = direction;
                manager.PlayerPosition = new Vector2(trans.Value.x, trans.Value.y);
            }
            ).Run();

        return default;
    }
}

using System.Collections;
using System.Collections.Generic;
using d4160.Core;
using d4160.GameFramework;
using UnityEngine;

public class PowerUpSpawnProvider : HybridSpawnProvider
{
    public override Vector3 SpawnPosition
    {
        get
        {
            var manager = SingleplayerModeManager.Instance.As<SingleplayerModeManager>();
            var xLimits = manager.PowerUpXLimits;
            var yLimits = manager.PowerUpYLimits;

            return new Vector3(xLimits.Random(), yLimits.y, 0f);
        }
    }
}

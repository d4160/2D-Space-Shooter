using d4160.Core;
using d4160.GameFramework;
using UnityEngine;

public class EnemySpawnProvider : HybridSpawnProvider
{
    public override Vector3 SpawnPosition
    {
        get
        {
            var manager = SingleplayerModeManager.Instance.As<SingleplayerModeManager>();
            var xLimits = manager.EnemyXLimits;
            var yLimits = manager.EnemyYLimits;

            return new Vector3(xLimits.Random(), yLimits.y, 0f);
        }
    }
}

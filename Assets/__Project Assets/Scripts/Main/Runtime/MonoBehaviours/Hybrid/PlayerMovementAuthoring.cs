using d4160.GameFoundation;
using d4160.GameFramework;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementAuthoring : MovementAuthoring, IMultipleStatUpgradeable
{
    protected override void OnUpdate(float dt)
    {
        DoMove(dt);
    }

    private void DoMove(float dt)
    {
        SingleplayerModeManager manager = SingleplayerModeManager.Instance.As<SingleplayerModeManager>();
        Vector2 xLimits = manager.PlayerXLimits;
        Vector2 yLimits = manager.PlayerYLimits;

        _data.direction = manager.PlayerDirection;

        var pos = transform.position;
        pos += (Vector3)(Vector2)_data.direction * _data.speed * dt;

        transform.position = manager.PlayerPosition = CalculateFixedPosition(pos, yLimits, xLimits);
    }

    private Vector3 CalculateFixedPosition(Vector3 pos, Vector2 yLimits, Vector2 xLimits)
    {
        Vector3 fixedPos = pos;

        fixedPos = new Vector3(
            fixedPos.x,
            Mathf.Clamp(fixedPos.y, yLimits.x, yLimits.y),
            fixedPos.z
        );

        if (fixedPos.x < xLimits.x)
            fixedPos.x = xLimits.y;
        else if (fixedPos.x > xLimits.y)
            fixedPos.x = xLimits.x;
        return fixedPos;
    }

    public void UpdateStat(int index, float value)
    {
        if (index != 1) return;

        _data.speed = value;
    }
}
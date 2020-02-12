using System.Collections;
using System.Collections.Generic;
using d4160.GameFramework;
using UnityEngine;
using UnityEngine.InputSystem;

public class SingleplayerModeManager : GameModeManagerBase
{
    [Header("PLAYER OPTIONS")]
    [SerializeField] protected Vector2 _playerXLimits = new Vector2(-11.3f, 11.3f);
    [SerializeField] protected Vector2 _playerYLimits = new Vector2(-3.8f, 0f);
    [Header("ENEMY OPTIONS")]
    [SerializeField] protected Vector2 _enemyXLimits = new Vector2(-8.5f, 8.5f);
    [SerializeField] protected Vector2 _enemyYLimits = new Vector2(-6.5f, 6.5f);

    private Vector2 _playerDirection;

    public Vector2 PlayerDirection => _playerDirection;
    public Vector2 PlayerXLimits => _playerXLimits;
    public Vector2 PlayerYLimits => _playerYLimits;
    public Vector2 PlayerPosition { get; set; }
    public Vector2 EnemyXLimits => _enemyXLimits;
    public Vector2 EnemyYLimits => _enemyYLimits;

    public void OnMoveAction(InputAction.CallbackContext ctx)
    {
        Vector2 inputValue = ctx.ReadValue<Vector2>();
        _playerDirection = inputValue;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using d4160.GameFoundation;
using d4160.GameFramework;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(HealthAuthoring), typeof(PlayerMovementAuthoring))]
[RequireComponent(typeof(MultipleStatCalculator))]
public class NetworkingPlayer : PUNEntityBehaviour
{
    public static GameObject LocalEntityInstance;

    [SerializeField] private MultipleStatCalculator _statCalculator;
    private InputValueActions _inputValueActions;
    private InputButtonActions _inputButtonActions;
    private HealthAuthoring _health;
    private HUDCanvas _hudCanvas;
    private SingleplayerModeManager _manager;
    private PlayerMovementAuthoring _movement;

    public HUDCanvas HUDCanvas
    {
        get
        {
            if (!_hudCanvas)
            {
                _hudCanvas = CanvasPrefabsManagerBase.Instance.InstancedMain as HUDCanvas;
            }

            return _hudCanvas;
        }
    }

    protected override void Awake()
    {
        _health = GetComponent<HealthAuthoring>();
        _inputValueActions = GetComponentInChildren<InputValueActions>();
        _inputButtonActions = GetComponentInChildren<InputButtonActions>();
        _movement = GetComponent<PlayerMovementAuthoring>();

        base.Awake();
    }

    protected void Start()
    {
        _manager = SingleplayerModeManager.Instance.As<SingleplayerModeManager>();

        if (PhotonView.IsMine)
        {
            _manager.SetPlayerStatCalculator(_statCalculator);
            _manager.SetPlayerInputButtonActions(_inputButtonActions);
        }

        _movement.enabled = PhotonView.IsMine;
    }

    protected void OnEnable()
    {
        if (PhotonView.IsMine)
        {
            _health.OnHealthUpdated.DynamicCalls += OnHealthUpdated;
            _statCalculator.OnStatUpdated.DynamicCalls += OnStatUpdated;
            _inputValueActions.AddVector2Callback(OnMovementInput, 0);
            _inputButtonActions.AddCallback(OnFire, 0);
            _inputButtonActions.AddCallback(OnRestart, 1);
        }
    }

    protected void OnDisable()
    {
        if (PhotonView.IsMine)
        {
            _health.OnHealthUpdated.DynamicCalls -= OnHealthUpdated;
            _inputValueActions.RemoveVector2Callback(OnMovementInput, 0);
            _inputButtonActions.RemoveCallback(OnFire, 0);
            _inputButtonActions.RemoveCallback(OnRestart, 1);
            _statCalculator.OnStatUpdated.DynamicCalls -= OnStatUpdated;
        }
    }

    protected void OnDestroy()
    {
        if (PhotonView.IsMine)
            PhotonNetwork.Destroy(this.gameObject);
    }

    private void OnStatUpdated(int index, float value)
    {
        if (index != 2) return;

        HUDCanvas.UpdateStat(index, value);
    }

    private void OnRestart(InputAction.CallbackContext obj)
    {
        _manager.LevelLoader.RestartLevel(LevelType.GameMode, 1);
    }

    private void OnFire(InputAction.CallbackContext obj)
    {
        _manager.PlayerLaserSpawnProvider.Spawn();
    }

    private void OnMovementInput(Vector2 dir)
    {
        _manager.SetPlayerDirection(dir);
    }

    private void OnHealthUpdated(int statIndex, float value)
    {
        HUDCanvas.UpdateStat(statIndex, value);
    }
}

using System.Collections;
using System.Collections.Generic;
using d4160.GameFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    [Range(0f, 15.5f)]
    [SerializeField] private float _speed = 5f;

    [Header("LASER OPTIONS")]
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private int _fireBySeconds = 3;
    [SerializeField] private Vector3 _instanceOffset = new Vector3(0f, .8f, 0f);
    [Range(0f, 31f)]
    [SerializeField] private float _overrideSpeed = 0f;
    
    [Header("HEALTH OPTIONS")]
    [SerializeField] private int _lives = 3;

    [Header("TALENT OPTIONS")] [SerializeField]
    private TalentGraph _talentGraph;

    private float _timeBetweenFires;
    private float _nextFireTime;
    private TalentGraphProcessor _graphProcessor;
    private GameObject _talentLaserPrefab;

    private bool CanFire => Time.time >= _nextFireTime;
    private GameObject LaserPrefab => _talentLaserPrefab ? _talentLaserPrefab : _laserPrefab;

    void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        transform.position = Vector3.zero;

        _timeBetweenFires = 1f / _fireBySeconds;

        if (_talentGraph && _graphProcessor == null)
        {
            _graphProcessor = new TalentGraphProcessor(_talentGraph);
        }

        UpdateTalents();
    }

    protected void UpdateTalents()
    {
        if (_graphProcessor != null)
        {
            _graphProcessor.Run();

            _talentLaserPrefab = _graphProcessor.LaserPrefab;
        }
    }

    void Update()
    {
        DoMove();

        var keyboard = Keyboard.current;
        if (keyboard.tKey.wasPressedThisFrame)
        {
            UpdateTalents();
        }
    }

    public void Fire()
    {
        if (!CanFire) return;
        ;
        var desiredPos = transform.position;
        desiredPos += _instanceOffset;

        var newLaser = Instantiate(
            LaserPrefab,
            desiredPos,
            Quaternion.identity);

        //if (_overrideSpeed > 0)
        //    newLaser.Speed = _overrideSpeed;

        _nextFireTime = Time.time + _timeBetweenFires;
    }

    private void DoMove()
    {
        SingleplayerModeManager manager = SingleplayerModeManager.Instance.As<SingleplayerModeManager>();
        Vector2 direction = manager.PlayerDirection;
        Vector2 xLimits = manager.PlayerXLimits;
        Vector2 yLimits = manager.PlayerYLimits;

        transform.Translate(direction * _speed * Time.deltaTime);

        var fixedPos = CalculateFixedPosition(yLimits, xLimits);

        transform.position = fixedPos;
    }

    private Vector3 CalculateFixedPosition(Vector2 yLimits, Vector2 xLimits)
    {
        Vector3 fixedPos = transform.position;

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

    public void Damage()
    {
        _lives--;
        if (_lives < 1)
        {
            EnemySpawnManager.Instance.StopSpawnRoutine();

            Destroy(gameObject);
        }
    }
}

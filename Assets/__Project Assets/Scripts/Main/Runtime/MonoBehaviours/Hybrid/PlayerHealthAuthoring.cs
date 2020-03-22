using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using AudioBehaviour = d4160.GameFramework.AudioBehaviour;

public class PlayerHealthAuthoring : HealthAuthoring
{
    [SerializeField] protected GameObject _explosion;
    [SerializeField] protected GameObject _leftEngine, _rightEngine;

    private AudioBehaviour _audio;

    protected override void Awake()
    {
        base.Awake();

        _audio = GetComponent<AudioBehaviour>();
    }

    protected override void DamageInternal(int damage)
    {
        base.DamageInternal(damage);

        if (_data.isInvulnerable)
            return;

        switch (_data.lives)
        {
            case 2:
                _leftEngine?.SetActive(true);
                break;
            case 1:
                _rightEngine?.SetActive(true);
                break;
        }
    }

    protected override void CheckForDestroy()
    {
        if (_data.lives < 1)
        {
            SingleplayerModeManager.Instance.As<SingleplayerModeManager>().StopSpawner();

            _explosion?.transform.SetParent(null);
            _explosion?.SetActive(true);
            
            _destroyable.DestroyInAdvance();
        }
        else
        {
            _audio.PlayAudioClip(2);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using d4160.GameFramework;
using UltEvents;
using UnityEngine;

public class AsteroidTriggerAuthoring : TriggerEnterEvent2D
{
    [SerializeField] protected GameObject _explosion;

    protected void Awake()
    {
        _explosion?.SetActive(false);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            _explosion?.transform.SetParent(null);
            _explosion?.SetActive(true);

            other.GetComponent<DefaultDestroyable>()?.Destroy();

            Destroy(gameObject, .25f);
        }

        base.OnTriggerEnter2D(other);
    }
}

using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

public class EnemyTriggerAuthoring : TriggerEnterEvent2D
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>()?.Damage();

            Destroy(gameObject);
        }
        else if (other.CompareTag("Laser"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }

        base.OnTriggerEnter2D(other);
    }
}

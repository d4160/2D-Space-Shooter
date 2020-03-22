using d4160.GameFoundation;
using d4160.GameFramework;
using UltEvents;
using UnityEngine;

[RequireComponent(
    typeof(DefaultDestroyable))] 
public class EnemyTriggerAuthoring : TriggerEnterEvent2D
{
    private DefaultDestroyable _destroyable;

    private void Awake()
    {
        _destroyable = GetComponent<DefaultDestroyable>();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (_destroyable.DestroyedState) return;
        
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthAuthoring>()?.Damage();

            _destroyable.DestroyInAdvance();
        }
        else if (other.CompareTag("Laser"))
        {
            var eCategory = other.GetComponentInParent<EntityCategoryAuthoring>();
            if (eCategory && eCategory.Category == 1)
                return;

            if (other.transform.parent)
            {
                other.gameObject.SetActive(false);
            }
            else
            {
                other.GetComponent<DefaultDestroyable>()?.Destroy();
            }

            SingleplayerModeManager.Instance.As<SingleplayerModeManager>().PlayerStatCalculator?.AddIntValueToStat(2, 10);

            _destroyable.DestroyInAdvance();
        }

        base.OnTriggerEnter2D(other);
    }
}

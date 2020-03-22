using d4160.GameFoundation;
using d4160.GameFramework;
using UltEvents;
using UnityEngine;
using UnityEngine.GameFoundation;

[RequireComponent(typeof(HealthAuthoring))]
public class PlayerTriggerAuthoring : TriggerEnterEvent2DExt
{
    private HealthAuthoring _health;

    private void Awake()
    {
        _health = GetComponent<HealthAuthoring>();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            InventoryItemBehaviour inventoryItem = other.GetComponent<InventoryItemBehaviour>();
            Inventory.main.AddItem(inventoryItem.InventoryItem);

            other.GetComponent<DefaultDestroyable>()?.Destroy();

            _selectedTriggerEnterEvent?.Invoke(0);
        }
        else if (other.CompareTag("Laser"))
        {
            var eCategory = other.GetComponentInParent<EntityCategoryAuthoring>();
            if (!eCategory || (eCategory && eCategory.Category == 0))
                return;

            if (other.transform.parent)
            {
                other.gameObject.SetActive(false);
            }
            else
            {
                other.GetComponent<DefaultDestroyable>()?.Destroy();
            }

            _health.Damage();
        }

        base.OnTriggerEnter2D(other);
    }
}

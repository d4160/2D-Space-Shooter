using d4160.GameFoundation;
using UltEvents;
using UnityEngine;
using UnityEngine.GameFoundation;

public class PlayerTriggerAuthoring : TriggerEnterEvent2D
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            InventoryItemBehaviour inventoryItem = other.GetComponent<InventoryItemBehaviour>();
            Inventory.main.AddItem(inventoryItem.InventoryItem);

            Destroy(other.gameObject);
        }

        base.OnTriggerEnter2D(other);
    }
}

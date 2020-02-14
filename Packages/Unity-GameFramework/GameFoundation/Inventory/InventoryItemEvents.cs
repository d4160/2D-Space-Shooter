using System.Collections;
using System.Collections.Generic;
using System.Linq;
using d4160.Core.Attributes;
using UltEvents;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.GameFoundation;

namespace d4160.GameFoundation
{
    public class InventoryItemEvents : MonoBehaviour
    {
        [Dropdown(ValuesProperty = "InventoryNames")]
        [SerializeField] protected int _inventory;

        [SerializeField] protected UnityEvent _onItemAdded;
        [SerializeField] protected UnityEvent _onItemRemoved;
        [SerializeField] protected UnityEvent _onItemQuantityChanged;

#if UNITY_EDITOR
        protected string[] InventoryNames =>
            InventoryManager.catalog.GetCollectionDefinitions().Select(x => x.displayName).ToArray();
#endif

        public Inventory SelectedInventory => InventoryManager.GetInventories()[_inventory];

        protected virtual void OnEnable()
        {
            Inventory inventory = SelectedInventory;

            inventory.onItemAdded += OnItemAdded;
            inventory.onItemRemoved += OnItemRemoved;
            inventory.onItemQuantityChanged += OnItemQuantityChanged;
        }

        protected virtual void OnDisable()
        {
            Inventory inventory = SelectedInventory;

            inventory.onItemAdded -= OnItemAdded;
            inventory.onItemRemoved -= OnItemRemoved;
            inventory.onItemQuantityChanged -= OnItemQuantityChanged;
        }

        protected virtual void OnItemAdded(InventoryItem item)
        {
            _onItemAdded?.Invoke(item);
        }

        protected virtual void OnItemRemoved(InventoryItem item)
        {
            _onItemRemoved?.Invoke(item);
        }

        protected virtual void OnItemQuantityChanged(InventoryItem item)
        {
            _onItemQuantityChanged?.Invoke(item);   
        }

        [System.Serializable]
        public class UnityEvent : UltEvent<InventoryItem>
        {
        }
    }
}

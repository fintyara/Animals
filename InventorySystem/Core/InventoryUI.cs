using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CollectCreatures
{
    public class InventoryUI : MonoBehaviour
    {
        public Inventory inventory;
        public Transform content;
        public ItemUI itemUIPrefab;

        public ItemEvent itemSelected;


        void Start()
        {
            if (inventory)
            {
                Display(inventory);
            }
        }
        public virtual void Display(Inventory i)
        {
            if (inventory)
            {
                inventory.onChanged.RemoveListener(Refresh);
            }
            this.inventory = i;
            inventory.onChanged.AddListener(Refresh);
            Refresh();
        }
        public virtual void Refresh()
        {
            foreach (Transform t in content)
            {
                Destroy(t.gameObject);
            }
            foreach (Item i in inventory.items)
            {
                ItemUI itemUI = ItemUI.Instantiate(itemUIPrefab, content);
                itemUI.onClicked.AddListener(UIClicked);
                itemUI.Display(i);
            }
        }
        public virtual void UIClicked(ItemUI itemUI)
        {
            itemSelected.Invoke(itemUI.item);
        }

        // V Code referenced by Unityevents only V
        #region UnityEventResponders

        public void AddToPlayerInventory(Item item)
        {
            // Inventory.playerInventory.TryAdd(item);
        }
        public void RemoveFromOwnInventory(Item item)
        {
            inventory.TryRemove(item);
        }
        public void Use(Item item)
        {
            if (!item.Use())
                RemoveFromOwnInventory(item);
        }
        public void Spawn(Item item)
        {
            AnimalItem animalItem = (AnimalItem)item;

            if (animalItem)
            {
                animalItem.Spawn();
                return;
            }
            else
                item.Spawn();
        }
        #endregion

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CollectCreatures
{
    public class AnimalsInventoryUI : MonoBehaviour
    {
        public AnimalsInventory animalsInventory;
        public Transform content;
        public AnimalItemUI animalItemUIPrefab;

        public AnimalItemEvent itemSelected;


        void Start()
        {
            if (animalsInventory)
            {
                Display(animalsInventory);
            }
        }
        public virtual void Display(AnimalsInventory i)
        {
            if (animalsInventory)
            {
                animalsInventory.onChanged.RemoveListener(Refresh);
            }
            this.animalsInventory = i;
            animalsInventory.onChanged.AddListener(Refresh);
            Refresh();
        }
        public virtual void Refresh()
        {
            foreach (Transform t in content)
            {
                Destroy(t.gameObject);
            }
            foreach (Item i in animalsInventory.items)
            {
                AnimalItemUI animalItemUI = AnimalItemUI.Instantiate(animalItemUIPrefab, content);
                animalItemUI.onClicked.AddListener(UIClicked);
                animalItemUI.Display(i);
            }
        }
        public virtual void UIClicked(AnimalItemUI animalItemUI)
        {
            itemSelected.Invoke(animalItemUI.animalItem);
        }

        // V Code referenced by Unityevents only V
        #region UnityEventResponders

        public void AddToPlayerInventory(AnimalItem animalItem)
        {
            // Inventory.playerInventory.TryAdd(item);
        }
        public void RemoveFromOwnInventory(AnimalItem animalItem)
        {
            animalsInventory.TryRemove(animalItem);
        }
        public void Use(AnimalItem animalItem)
        {
            if (!animalItem.Use())
                RemoveFromOwnInventory(animalItem);
        }
        public void Spawn(AnimalItem animalItem)
        {
            if (animalItem.Spawn())
                RemoveFromOwnInventory(animalItem);
        }
        #endregion
    }
}

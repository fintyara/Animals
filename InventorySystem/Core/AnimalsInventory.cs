using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CollectCreatures
{
    public class AnimalsInventory : Inventory
    {
        public new List<AnimalItem> items;


        public bool TryAdd(AnimalItem animalItem)
        {
            if (items.Count < maxItems)
            {
                items.Add(animalItem);
                onChanged.Invoke();
                return true;
            }
            else
                return false;
        }
        public bool TryRemove(AnimalItem animalItem)
        {
            if (!items.Contains(animalItem))
                return false;

            items.Remove(animalItem);
            onChanged.Invoke();

            return true;
        }
    }
}

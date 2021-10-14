using System.Collections.Generic;
using UnityEngine;

namespace CollectCreatures
{ 
    [CreateAssetMenu(menuName = "InventorySystem/ItemList")]
    public class ItemList : ScriptableObject
    {
        public List<Item> Items => _items;
        [SerializeField] private List<Item> _items = new List<Item>();
    }
}

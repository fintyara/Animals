using System.Collections.Generic;
using UnityEngine;

namespace CollectCreatures
{
    [CreateAssetMenu(menuName = "InventorySystem/ToolsLootbox")]
    public class ToolsLootbox : ScriptableObject
    {
        #region VAR
        [SerializeField] public List<Item> Items => _items;
        public RarityType RarityType => _rarityType;
        public int Price => _price;

        [SerializeField] private List<Item> _items = new List<Item>();
        [SerializeField] private List<int> _itemsChance = new List<int>();
        [SerializeField] private RarityType _rarityType;
        [SerializeField] private int _price = 100;
        private int allChance;
        #endregion

        #region FUNC
       
        public Item GetItem()
        {
            if (_items.Count != _itemsChance.Count)
            {
                Debug.LogError("Need same length");
                return null;
            }
            
            allChance = 0;

            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Rarity == _rarityType)
                {
                    allChance += _itemsChance[i] * 2;
                }
                else
                {
                    allChance += _itemsChance[i];
                }
            }
            
            var random = Random.Range(1, allChance + 1);
            
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Rarity == _rarityType)
                {
                    allChance -= _itemsChance[i] * 2;
                }
                else
                {
                    allChance -= _itemsChance[i];
                }
                
                if (random >= allChance)
                {
                    return _items[i];
                }
            }

            return null;
        }
        #endregion
        
    }
}
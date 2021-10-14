using System.Collections;
using System.Collections.Generic;
using CollectCreatures;
using UnityEngine;
using UnityEngine.Events;
using SuperMaxim.Messaging;


[System.Serializable]
public class ItemEvent : UnityEvent<Item> { };

[CreateAssetMenu(menuName = "InventorySysem/Item")]
public class Item : ScriptableObject
{
    [SerializeField] protected Sprite _ico;
    public Sprite Ico => _ico;
    
    [SerializeField] protected RarityType _rarity;
    public RarityType Rarity => _rarity;

    [SerializeField] protected GameObject _prefab;
    public GameObject Prefab => _prefab;

    [SerializeField] protected List<ItemEffect> _itemEffects = new List<ItemEffect>();
    public List<ItemEffect> ItemEffects => _itemEffects;

    [SerializeField] protected bool _consumable = true;
    public bool Consumable => _consumable;

    [SerializeField] protected bool _spawnable = true;
    public bool Spawnable => _spawnable;


    public virtual bool Use()
    {
        for (int i = 0; i < _itemEffects.Count; i++)
        {
            _itemEffects[i].Use();
        }

        return !_consumable;
    }
    public virtual bool Spawn()
    {
        Debug.Log("spawn item");
        Messenger.Default.Publish(new ItemSpawnPayload { item = this });

        return _spawnable;
    }
}

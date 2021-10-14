using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "InventorySysem/Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> items;
    public int maxItems = 8;

    public UnityEvent onChanged;


    public virtual bool TryAdd(Item item)
    {
        if (items.Count < maxItems)
        {
            items.Add(item);
            onChanged.Invoke();
            return true;
        }
        else
            return false;              
    }
    public virtual bool TryRemove(Item item)
    {
        if (!items.Contains(item))
            return false;

        items.Remove(item);
        onChanged.Invoke();

        return true;
    }
    public virtual bool HaveSpace()
    {
        if (items.Count < maxItems)
            return true;
        else
            return false;
    }
}

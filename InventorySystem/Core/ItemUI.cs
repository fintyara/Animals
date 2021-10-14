using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[System.Serializable]
public class ItemUIEvent : UnityEvent<ItemUI> { };


public class ItemUI : MonoBehaviour
{
    public Item item;
    public Image itemImage;
    GameObject SpawnedPrefab;

    public ItemUIEvent onClicked;

    void Start()
    {
        if (item)
            Display(item);
    }

    public virtual void Display(Item item)
    {
        this.item = item;
        itemImage.enabled = true;
        itemImage.sprite = item.Ico;
    }
    public virtual void Click()
    {
        onClicked.Invoke(this);
    }
}

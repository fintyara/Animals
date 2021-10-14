using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace CollectCreatures
{
    [System.Serializable]
    public class AnimalItemUIEvent : UnityEvent<AnimalItemUI> { };

    public class AnimalItemUI : ItemUI
    {
        public AnimalItem animalItem;
        public new AnimalItemUIEvent onClicked;

        void Start()
        {
            if (item)
                Display(item);
        }

        public void Display(AnimalItem animalItem)
        {
            this.animalItem = animalItem;
            itemImage.enabled = true;
            itemImage.sprite = item.Ico;
        }
        public override void Click()
        {
            onClicked.Invoke(this);
        }
    }
}

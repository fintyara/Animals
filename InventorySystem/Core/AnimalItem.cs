using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SuperMaxim.Messaging;

namespace CollectCreatures
{
    [System.Serializable]
    public class AnimalItemEvent : UnityEvent<AnimalItem> { };

    [CreateAssetMenu(menuName = "InventorySysem/AnimalItem")]
    public class AnimalItem : Item
    {
        #region VAR
        public int Id;
        public AnimalType AnimalType => _animalType;
        [SerializeField] private AnimalType _animalType;
        #endregion
        
        public override bool Use()
        {
            return false;
        }
        public override bool Spawn()
        {
            Messenger.Default.Publish(new AnimalSpawnPayload { animalItem = this });

            return _spawnable;
        }
    }
}

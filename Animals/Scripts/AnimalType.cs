using UnityEngine;

namespace CollectCreatures
{
    [CreateAssetMenu(menuName = "Types/AnimalType")]
    public class AnimalType : ScriptableObject
    {
        #region VAR
        public AnimalClass AnimalClass => _animalClass;
        public FoodType FoodType => _foodType;
        public Sprite Ico => _ico;
        public GameObject Prefab => _prefab;

        public float speedWalk => _speedWalk;
        public float speedHungry => _speedHungry;
        public int income => _income;
        public int timeSleep => _timeToSleep;
        public int timeWakeUp => _timeToWakeUp;
        
        public int id;
       
        [SerializeField] private AnimalClass _animalClass;
        [SerializeField] private FoodType _foodType;
        [SerializeField] private Sprite _ico;
        [SerializeField] private GameObject _prefab;
        [Space(10)]
        [SerializeField] private float _speedWalk;
        [SerializeField] private float _speedHungry;
        [SerializeField] private int _income;
        [SerializeField] private int _timeToSleep = 23;
        [SerializeField] private int _timeToWakeUp = 5;
        #endregion
    }
}

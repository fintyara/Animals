using UnityEngine;


namespace CollectCreatures
{
    [CreateAssetMenu(menuName = "Types/FoodType")]
    public class FoodType : ScriptableObject
    {
        public FoodClass FoodClass => foodClass;
        [SerializeField] private FoodClass foodClass;
        [Space(10)]
        [SerializeField] private GameObject prefab;
    }
}
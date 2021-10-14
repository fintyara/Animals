using UnityEngine;
using UnityEngine.Events;

namespace CollectCreatures
{
    public abstract class Food : MonoBehaviour
    {
        #region VAR
        public UnityEvent onFull;
        public UnityEvent onEmpty;

        protected const int BEST_FOOD_FULLNESS = 150;
        protected const int GOOD_FOOD_FULLNESS = 100;
        protected const int BAD_FOOD_FULLNESS = 70;
        
        public FoodType FoodType => foodType;
        public bool HaveFood => _haveFood;
        public bool Busy => _busy;
        [SerializeField] protected FoodType foodType;
        [SerializeField] protected GameObject _fruitView;
        [ShowOnly] [SerializeField] protected bool _haveFood;
        [ShowOnly] [SerializeField] protected bool _busy;
        #endregion
        
        #region FUNC
        public abstract Food GetFood();
        public abstract int EatFood(FoodType neededType);
        public abstract void AddFood();
        public abstract void SetFree();
        public abstract void DeleteFood();
        #endregion   
    }
}

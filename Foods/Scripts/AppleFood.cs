using System;
using UnityEngine;

namespace CollectCreatures
{
    public class AppleFood : Food
    {
        #region FUNC
        public override Food GetFood()
        {
            _busy = true;
            return this;
        }
        public override int EatFood(FoodType neededType)
        {
            _haveFood = false;
            _busy = false;
            onEmpty?.Invoke();
            _fruitView.SetActive(false);
            
            if (foodType == neededType)
                return BEST_FOOD_FULLNESS;
            
            return foodType == neededType ? GOOD_FOOD_FULLNESS : BAD_FOOD_FULLNESS;
        }
        public override void AddFood()
        {
            _haveFood = true;
            onFull?.Invoke();
            _fruitView.SetActive(true);
        }
        public override void DeleteFood()
        {
            _haveFood = false;
            onEmpty?.Invoke();
            _fruitView.SetActive(false);
        }
        public override void SetFree()
        {
            _busy = false;
        }
        #endregion
    }
}

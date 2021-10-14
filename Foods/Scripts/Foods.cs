using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CollectCreatures
{
    public class Foods : MonoBehaviour
    {
        #region VAR
        private List<HaveFood> _haveFoods = new List<HaveFood>();
        private List<HaveFood> _selected = new List<HaveFood>();
        private List<float> _distances = new List<float>();
        #endregion

        #region MONO
        private void Start()
        {
            FindAllFoodPlace();
        }
        #endregion
        
        #region FUNC
        public Food FindFood(FoodType foodType, Vector3 originalPos, float maxDistance)
        {
            _selected.Clear();
            _distances.Clear();

            SelectByHaveFoodAndMaxDistance(originalPos, maxDistance);

            if (_selected.Count == 0)
                return null;
          
            var haveFood = SelectNear(maxDistance);
            if (haveFood != null)
                return haveFood.GetFood();
            
            SortByDistance(originalPos);

            haveFood = SelectByFoodType(foodType);
            if (haveFood != null)
                return haveFood.GetFood();
            
            haveFood = SelectByFoodClass(foodType);
            if (haveFood != null)
                return haveFood.GetFood();

            return SelectAnyFood().GetFood();
        }
        public void AddFoodPlace(HaveFood foodPlace)
        {
            if(!_haveFoods.Contains(foodPlace))
                _haveFoods.Add(foodPlace);
        }
        
        private void SelectByHaveFoodAndMaxDistance(Vector3 originalPos, float maxDistance)
        {
            for (int i = 0; i < _haveFoods.Count; i++)
            {
                var dist = Vector3.Distance(originalPos, _haveFoods[i].transform.position);
                if (_haveFoods[i].CheckHaveFood() && dist < maxDistance)
                {
                    _selected.Add(_haveFoods[i]);
                    _distances.Add(dist);
                }
            }
        }
        private void SortByDistance(Vector3 originalPos)
        {
            _selected = _selected
                .OrderBy(x => (x.transform.position - originalPos).sqrMagnitude)
                .ToList();
        }
        private HaveFood SelectNear(float maxDistance)
        {
            for (int i = 0; i < _distances.Count; i++)
            {
                if (_distances[i] < maxDistance / 3)
                    return _haveFoods[i];
            }

            return null;
        }
        private HaveFood SelectByFoodType(FoodType foodType)
        {
            for (int i = 0; i < _selected.Count; i++)
            {
                if (_haveFoods[i].FoodType == foodType)
                    return _haveFoods[i];
            }

            return null;
        }
        private HaveFood SelectByFoodClass(FoodType foodType)
        {
            for (int i = 0; i < _selected.Count; i++)
            {
                if (_haveFoods[i].FoodType.FoodClass == foodType.FoodClass)
                    return _haveFoods[i];
            }

            return null;
        }
        private HaveFood SelectAnyFood()
        {
            return _haveFoods[0];
        }
        private void FindAllFoodPlace()
        {
            var places = GameObject.FindObjectsOfType<HaveFood>();

            if (places.Length == 0)
                return;
            
            for (int i = 0; i < places.Length; i++)
            {
                _haveFoods.Add(places[i]);
            }
        }
        #endregion  
    }
}

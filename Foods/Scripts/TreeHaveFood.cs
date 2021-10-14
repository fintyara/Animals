using System.Collections.Generic;
using UnityEngine;


namespace CollectCreatures
{
    public class TreeHaveFood : HaveFood
    {
        #region VAR
        [SerializeField] private List<AppleFood> _appleFoods = new List<AppleFood>();
        private List<int> _selected = new List<int>();
        #endregion
        
        #region FUNC
        public override bool CheckHaveFood()
        {
            for (int i = 0; i < _appleFoods.Count; i++)
            {
                if (_appleFoods[i].HaveFood && !_appleFoods[i].Busy)
                    return true;
            }
            
            return false;
        }
        public override Food GetFood()
        { 
            for (int i = 0; i < _appleFoods.Count; i++)
            {
                if (_appleFoods[i].HaveFood && !_appleFoods[i].Busy)
                    return _appleFoods[i].GetFood();
            }
            
            return null;
        }
        public override void AddFood()
        {
            Food emptyFood = GetEmptyFood();

            if (emptyFood == null)
                return;
            
            emptyFood.AddFood();
        }
        public override void DeleteOneFood(int count)
        {
            for (int i = 0; i < _appleFoods.Count; i++)
            {
                if (_appleFoods[i].HaveFood)
                {
                    _appleFoods[i].DeleteFood();
                    return;
                }
            }
        }
        public override void DeleteAllFood()
        {
            for (int i = 0; i < _appleFoods.Count; i++)
            {
                if (_appleFoods[i].HaveFood)
                {
                    _appleFoods[i].DeleteFood();
                }
            }
        }
        private Food GetEmptyFood()
        {
            _selected.Clear();
            
            for (int i = 0; i < _appleFoods.Count; i++)
                if (!_appleFoods[i].HaveFood)
                    _selected.Add(i);

            if (_selected.Count == 0)
                return null;

            var randomIndex = _selected[Random.Range(0, _selected.Count)];

            return _appleFoods[randomIndex];
        }
        #endregion
    }
}

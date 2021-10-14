using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CollectCreatures
{
    public class EatIState : StateBase
    {
        #region VAR
        public UnityEvent onBeginEat;
        public IntEvent onEndEat;

        public bool NeedEat => _needEat;
        [SerializeField] private FoodType _foodType;
        [SerializeField] private float _speedUpFactor = 1.2f;
        [SerializeField] private float _distanceFind = 50;
        [SerializeField] private float _distanceEat = 1;
        [SerializeField] private float _durationPrepareAction;
        [SerializeField] private float _durationAction;
        
        [ShowOnly] [SerializeField] private bool _needEat;
        [ShowOnly] [SerializeField] private bool _actionRun;

        private ICanMove _canMove;
        private Foods _foods;
        private Food _food;
        #endregion

        #region MONO
        private void Start()
        {
            _foods = GameObject.Find("Foods").GetComponent<Foods>();
            if (_foods == null)
                Debug.Log("Need Foods component");
            
            _canMove = GetComponentInParent<ICanMove>();
            if (_canMove == null)
                Debug.Log("Need CanMove component");
        }
        private void OnDestroy()
        {
            if (_food != null)
                _food.onEmpty.RemoveListener((FoodIsEmpty));
        }
        #endregion

        #region FUNC
        public override void EnterState()
        {
            _elastedTime = 0;
            _active = true;

            _food = _foods.FindFood(_foodType, transform.position, _distanceFind);
            if (_food == null)
                _finish = true;
            else
            {
                StartMove();
                _food.onEmpty?.AddListener((FoodIsEmpty));
            }
        }
        public override void State()
        {
            _elastedTime += Time.deltaTime;
        }
        public override void ExitState()
        {
           ResetMove();
           ResetTarget();
           
            _active = false;
            _finish = false;
        }
        
        private void StartMove()
        { 
            _canMove.StartMove(_food.transform, _distanceEat, _speedUpFactor);
            _canMove.OnDestinationTarget.AddListener(StartAction);
        }
        private void StartAction()
        {
            StartCoroutine(ActionRoutine());
        }
        private IEnumerator ActionRoutine()
        {
            _actionRun = true;

            yield return new WaitForSeconds(_durationPrepareAction);

            if (_food != null && _food.HaveFood)
            {
                onBeginEat?.Invoke();
                
                yield return new WaitForSeconds(_durationAction);

                if (_food != null && _food.HaveFood)
                {
                    onEndEat?.Invoke(_food.EatFood(_foodType));
                    _needEat = false;
                }
            }
            _actionRun = false;
            _finish = true;
        }
        
        private void FoodIsEmpty()
        {
            _finish = true;
        }
        private void ResetMove()
        {
            _canMove.OnDestinationTarget.RemoveListener(StartAction);
            _canMove.Finish(); 
        }
        private void ResetTarget()
        {
            if (_food != null)
            {
                _food.SetFree();
            }
            _food = null;
        }
        #endregion
        
        // V Code referenced by UnityEvents only V 
        public void NeedToEat()
        {
            _needEat = true;
        }
    }
}

using UnityEngine;

namespace CollectCreatures
{
    public class WanderState : StateBase
    {
        #region VAR
        [SerializeField] private float _maxDistance;
        [ShowOnly][SerializeField] private float _curDistance;
        private ICanWander _canWander;
        #endregion

        #region MONO
        private void Start()
        {
            _canWander = GetComponentInParent<ICanWander>();
            if (_canWander == null)
                Debug.Log("Need ICanWander interface");
        }
        private void OnDestroy()
        {
            _canWander?.OnDestinationTarget.RemoveListener(FinishWander);
        }
        #endregion

        #region FUNC
        public override void EnterState()
        {
            _elastedTime = 0;
            _active = true;
            
            _curDistance = Random.Range(_maxDistance / 2, _maxDistance);
            _canWander.StartWander(_curDistance, 1, 1);
            _canWander.OnDestinationTarget.AddListener(FinishWander);
        }
        public override void State()
        {
            if (!_finish && _elastedTime > _duration)
                _finish = true;
            
            _elastedTime += Time.deltaTime;
        }
        public override void ExitState()
        {
            _canWander.Finish();
            _canWander.OnDestinationTarget.RemoveListener(FinishWander);
            
            _finish = false;
            _active = false;
        }
        private void FinishWander()
        {
            _finish = true;
        }
        #endregion
    }
}

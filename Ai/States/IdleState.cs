using UnityEngine;

namespace CollectCreatures
{
    public class IdleState : StateBase
    {
        #region VAR
        [SerializeField] protected float _chanceToStay;
        #endregion
        
        #region FUNC
        public override void EnterState()
        {
            _active = true;

            if (Random.Range(0, 100) < _chanceToStay)
                _finish = true;
        }
        public override void State()
        {
            if (!_finish && _elastedTime > _duration)
                _finish = true;
            
            _elastedTime += Time.deltaTime;
        }
        public override void ExitState()
        {
            _active = false;
            _finish = false;
        }
        #endregion
   
    }
}

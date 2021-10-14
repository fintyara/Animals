using UnityEngine;

namespace CollectCreatures
{
    public class OnAttackReady : TransitionBase
    {
        #region VAR
        private AttackState _attackState;
        #endregion
        
        #region MONO
        private void Start()
        {
            _attackState = GetComponent<AttackState>();
            if (_attackState == null)
                Debug.Log("Need AttackIState component");
        }
        #endregion

        public override bool Condition(StateBase curState)
        {
            return _attackState.Target != null && _attackState.CanAttack;
        }
    }
}
using UnityEngine;

namespace CollectCreatures
{
    public class OnHaveDanger : TransitionBase
    {
        #region VAR
        private FleeState _fleeState;
        #endregion
        
        #region MONO
        private void Start()
        {
            _fleeState = GetComponent<FleeState>();
            if (_fleeState == null)
                Debug.Log("Need FleeIState component");
        }
        #endregion

        public override bool Condition(StateBase curState)
        {
            return _fleeState.DangerEntity != null;
        }
    }
}

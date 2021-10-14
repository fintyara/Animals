using System.Collections.Generic;
using UnityEngine;

namespace CollectCreatures
{
    public class OnFinishIdle : TransitionBase
    {
        #region VAR
        private new List<StateBase> FromStates;
        private IdleState _idleState;
        #endregion

        #region MONO
        private void Start()
        {
            _idleState = GetComponent<IdleState>();
            if (_idleState == null)
                Debug.Log("Need IdleIState component");
        }
        #endregion
        
        #region FUNC
        public override bool Condition(StateBase curState)
        {
            return _idleState.Finish;
        }
        #endregion   
    }
}

using System;
using UnityEngine;

namespace CollectCreatures
{
    public class OnNeedEat : TransitionBase
    {
        #region VAR
        private EatIState _eatState;
        #endregion
        
        #region MONO
        private void Start()
        {
            _eatState = GetComponent<EatIState>();
            if (_eatState == null)
                Debug.Log("Need EatIState component");
        }
        #endregion
        
        #region FUNC
        public override bool Condition(StateBase curState)
        {
            return curState.Finish && _eatState.NeedEat;
        }
        #endregion   
    }
}

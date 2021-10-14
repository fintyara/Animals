using UnityEngine;

namespace CollectCreatures
{
    public class OnFinish : TransitionBase
    {
        #region FUNC
        public override bool Condition(StateBase curState)
        {
            return curState.Finish;
        }
        #endregion
    }
}

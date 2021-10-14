using System;
using System.Collections.Generic;
using UnityEngine;

namespace CollectCreatures
{
    public class FSM : MonoBehaviour
    {
        #region VAR
        public event Action<int> OnStateIdChanged;
        public event Action<StateTag> OnStateChanged;

        [SerializeField] protected List<StateBase> _states = new List<StateBase>();
        public List<TransitionBase> Transitions => _transitions;
        [Space(10)] [SerializeField] protected List<TransitionBase> _transitions;
        [Space(10)] [ShowOnly] [SerializeField]
        protected bool locked;
        [ShowOnly] [SerializeField] protected StateBase curState;
        #endregion

        #region MONO
        private void Start()
        {
            if (_states.Count > 0)
                SetStateByIndex(0);
        }
        private void Update()
        {
            CheckTransitions();
            curState.State();
        }
        #endregion

        #region FUNC
        public void CheckTransitions()
        {
            for (int i = 0; i < Transitions.Count; i++)
            {
                if ((Transitions[i].FromStates.Count == 0 || Transitions[i].FromStates.Contains(curState)) &&
                    Transitions[i].Condition(curState))
                    SetState(Transitions[i].ToState);
            }
        }
        

        public void SetStateByTag(StateTag newStateTag)
        {
            SetState(GetStateByTag(newStateTag));
        }
        public void SetStateByIndex(int index)
        {
            SetState(_states[index]);
        }
        public void SetState(StateBase newState)
        {
            if (locked || newState == null || curState == newState)
                return;
            if (!_states.Contains(newState))
                return;

            curState?.ExitState();
            curState = newState;
            curState.EnterState();

            OnStateIdChanged?.Invoke(_states.IndexOf(curState));
            OnStateChanged?.Invoke(curState.Tag);
        }

        public int GetStateId()
        {
            return _states.IndexOf(curState);
        }
        public StateTag GetStateTag()
        {
            return curState.Tag;
        }
        public void LockState()
        {
            locked = true;
        }
        public void UnlockState()
        {
            locked = false;
        }
        private StateBase GetStateByTag(StateTag newStateTag)
        {
            for (int i = 0; i < _states.Count; i++)
            {
                if (_states[i].Tag == newStateTag)
                    return _states[i];
            }

            return null;
        }
        #endregion
    }
}

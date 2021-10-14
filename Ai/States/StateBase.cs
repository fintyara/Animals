using System.Collections.Generic;
using UnityEngine;

namespace CollectCreatures
{
    public class StateBase : MonoBehaviour, IState
    {
        #region VAR
        public bool Active => _active;
        public bool Finish => _finish;
        public StateTag Tag => tagStateTag;
        public float Duration => _duration;
        public float ElastedTime => _elastedTime;
        [SerializeField] protected StateTag tagStateTag;
        [Space(10)][SerializeField] protected float _duration;
        [ShowOnly] [SerializeField] protected bool _active;
        [ShowOnly] [SerializeField] protected bool _finish;
        [ShowOnly] [SerializeField] protected float _elastedTime;
        #endregion
        
   
        #region FUNC
        public virtual void EnterState()
        {
            _elastedTime = 0;
            _active = true;
        }
        public virtual void State()
        {
            _elastedTime += Time.deltaTime;
        }
        public virtual void ExitState()
        {
            _active = false;
        }
        #endregion
    }
}

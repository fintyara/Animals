using CollectCreatures;
using UnityEngine;


namespace CollectCreatures
{
    public abstract class ContinuousEffect : MonoBehaviour, IEffect
    {
        #region VAR
        public string TargetUid => targetUid;
        public bool Active
        {
            get => active;
            set => active = value;
        }
        public EffectType EffectType => effectType;
        public int Priority => priority;
        [Space(10)]
        [ShowOnly][SerializeField] protected string targetUid;
        [SerializeField] protected Sprite ico;
        [SerializeField] protected EffectType effectType;
        [SerializeField] protected int priority;

        [SerializeField] protected int power = 30;
        [SerializeField] protected float duration = 10;
        [SerializeField] protected float delayApplyEffect = 0.5f;
        [ShowOnly] [SerializeField] protected bool active;
        [ShowOnly] [SerializeField] protected float timeElapsed;
        [ShowOnly] [SerializeField] protected bool initialized;
        protected Entity originalEntity;
        protected Entity targetEntity;
        #endregion

        #region FUNC
        protected abstract void  DurationControl();
        protected abstract void  Clear();
        protected abstract void  Break();
        
        protected virtual void UpdateEffect(){}
        #endregion
        
        #region INTERFACES
        public abstract void Init(Entity original, Entity target);
        public abstract void ApplyEffect();
        #endregion
    }
}

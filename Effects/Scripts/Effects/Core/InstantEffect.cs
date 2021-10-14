using CollectCreatures;
using UnityEngine;


namespace CollectCreatures
{
    public abstract class InstantEffect : MonoBehaviour, IEffect
    {
        #region VAR
        [SerializeField] protected float delayApplyEffect = 0.5f;
        protected Entity originalEntity;
        protected Entity targetEntity;

        #endregion

        #region FUNC
        public abstract void Init(Entity original, Entity target);
        public abstract void ApplyEffect();
        #endregion
    }
}

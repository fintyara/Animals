using UnityEngine;

namespace CollectCreatures
{
    public class DeathEffect : InstantEffect
    {
        #region FUNC
        public override void Init(Entity original, Entity target)
        {
            Invoke(nameof(ApplyEffect), delayApplyEffect);
        }
        public override void ApplyEffect()
        {
            ApplySetState();
            Destroy(gameObject);
        }
        private void ApplySetState()
        {
            var entity = GetComponentInParent<Entity>();

            if (entity != null)
                entity.Death();
        }
        #endregion
    }
}

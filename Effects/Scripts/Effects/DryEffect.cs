using UnityEngine;


namespace CollectCreatures
{
    public class DryEffect : InstantEffect
    {
        #region VAR
        [SerializeField] private StateTag targetStateTag;
        [SerializeField] private float powerModifySpeedGrouth = 40;
        #endregion
        
        #region FUNC
        public override void Init(Entity original, Entity target)
        {
            Invoke(nameof(ApplyEffect), delayApplyEffect);
        }
        public override void ApplyEffect()
        {
            var haveState = transform.parent.GetComponentInChildren<IHaveState>();

            if (haveState != null && haveState.GetState() != targetStateTag)
            {
                haveState?.SetState(targetStateTag);
                
                var canGrouth = GetComponentInParent<CanGrouth>();
            
                if (canGrouth != null)
                    canGrouth.ModifyReload(powerModifySpeedGrouth);
            }
            
            Destroy(gameObject);
        }
        #endregion
    }
}

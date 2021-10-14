using UnityEngine;

namespace CollectCreatures
{
    public class ScareEffect : ContinuousEffect
    {
        #region VAR
        private HaveMood _haveMood;
        private FleeState _fleeState;
        private Entity _entity;
        #endregion
        
        #region MONO
        private void Update()
        {
            if (active)
            {
                DurationControl();
            }
        }
        private void OnDisable()
        {
           Clear();
        }
        #endregion
        
        #region FUNC
        public override void Init(Entity original, Entity target)
        {
            targetUid = target.Uid;
            originalEntity = original;
            targetEntity = target;
            
            _fleeState = transform.parent.GetComponentInChildren<FleeState>();
            _haveMood = transform.GetComponentInParent<HaveMood>();

            if (_fleeState == null && _haveMood == null)
            {
                Debug.Log("Need FleeAiState or HaveMood component");
                Destroy(gameObject);
            }

            _entity = GetComponentInParent<Entity>();
                 
            initialized = true;


            Invoke("ApplyEffect", delayApplyEffect);
        }
       
        protected override void DurationControl()
        {
            if (timeElapsed > duration)
            {
                active = false;
                Destroy(gameObject);
            }
            else
            {
                timeElapsed += Time.deltaTime;
            }
        }
        protected override void Clear()
        {
            if (_entity != null)
                _entity.ClearEffect(effectType);
            if (_haveMood != null)
                _haveMood.MoodUp(power);
        }
        protected override void Break()
        {
            duration = 0;
        }
        #endregion
        
        #region INTERFACES
     
        public override void ApplyEffect()
        {
            if (_fleeState != null)
                _fleeState.DangerEntity = originalEntity;
            if (_haveMood != null)
                _haveMood.MoodDown(power);

            active = true;
            originalEntity = null;
        }
        #endregion
    }
}

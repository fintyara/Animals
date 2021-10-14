using UnityEngine;
using UnityEngine.Events;

namespace CollectCreatures
{
    public class FleeState : StateBase
    {
        #region VAR
        public UnityEvent OnFlee;
        public Entity DangerEntity;
        
        [SerializeField] private float speedUpFactor = 1.5f;
        [SerializeField] protected float escapedDistance = 30;
        [SerializeField] protected float findDistance = 30;
      
        private ICanFlee _canFlee;
        #endregion
        
        #region MONO
        private void Start()
        {
            _canFlee = GetComponentInParent<ICanFlee>();
            if (_canFlee == null)
                Debug.Log("Need ICanFlee interface");
        }
        private void OnDestroy()
        {
            if (DangerEntity != null)
                DangerEntity.onDeath.RemoveListener(DangerDestroyed);
        }
        #endregion

        #region FUNC
        public override void EnterState()
        {
            _elastedTime = 0;
            _active = true;

            StartMove();
            DangerEntity.onDeath.AddListener(DangerDestroyed);
        }
        public override void State()
        {
            if (!_finish && _elastedTime > _duration)
                _finish = true;
            
            _elastedTime += Time.deltaTime;
        }
        public override void ExitState()
        {
           ResetMove();
           ResetTarget();
           
            _active = false;
            _finish = false;
        }
        
        private void DangerDestroyed(Entity e)
        {
            DangerEntity = null;
            _finish = true;
        }
        private void StartMove()
        {
            _canFlee.StartEscape(DangerEntity.transform, escapedDistance, findDistance, speedUpFactor);
            _canFlee.OnEscaped.AddListener(IsEscaped);
        }
        private void IsEscaped()
        {
            _finish = true;
        }
        private void ResetMove()
        {
            _canFlee.OnEscaped.RemoveListener(IsEscaped);
            _canFlee.Finish(); 
        }
        private void ResetTarget()
        {
            if (DangerEntity != null)
            {
                DangerEntity.onDeath.AddListener(DangerDestroyed);
            }
            DangerEntity = null;
        }
        #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CollectCreatures
{
    [System.Serializable]
    public class EntityTypeEvent : UnityEvent<EntityType> { };
    
    public class AttackState : StateBase
    {
        #region VAR    

        public UnityEvent OnAttack;
        public EntityTypeEvent OnEntityTypeChanged; 
        public bool CanAttack => _canAttack;
        public Entity Target => _target;
        [Space(10)]
        [SerializeField] private AttackType _attackType;
        [SerializeField] private float _reloadAttack;
        [SerializeField] private float _powerAttack;
        [SerializeField] private float _speedUpFactor = 1.2f;
        [SerializeField] private float _findDistance = 100;
        [SerializeField] private float _attackDistance;
        [SerializeField] private float _durationPrepareAction;
        [SerializeField] private float _durationAction;
        [SerializeField] protected LayerMask _enemiesLayerMask;
        
        [ShowOnly] [SerializeField] private bool _canAttack;
        [ShowOnly] [SerializeField] private bool _actionRun;
        [ShowOnly] [SerializeField] private float _timeAttack;

        private List<Entity> _findedEntities = new List<Entity>();
        private Entity _parentEntity;
        private Entity _target;
        private ICanMove _canMove;
        #endregion

        #region MONO
        private void Start()
        {
            _parentEntity = GetComponentInParent<Entity>();
            if (_parentEntity == null)
                Debug.Log("Need Entity component");
            
            _canMove = GetComponentInParent<ICanMove>();
            if (_canMove == null)
                Debug.Log("Need CanMove component");
            
            InvokeRepeating(nameof(FindTarget), 0.5f, 0.5f);
        }
        private void Update()
        {
            if (!_canAttack && Time.time > _timeAttack + _reloadAttack)
            {
                _canAttack = true;
            }
        }
        private void OnDestroy()
        {
            if (_target != null)
                _target.onDeath.RemoveListener(TargetDestroyed);
        }
        #endregion

        #region FUNC
        public override void EnterState()
        {
            _elastedTime = 0;
            _active = true;
            
            if (_target != null)
            {
                StartMove();
            }
            else
            {
                _finish = true;
            }
        }
        public override void State()
        {
            if (!_finish && _elastedTime > _duration)
                _finish = true;
            
            _elastedTime += Time.deltaTime;
        }
        public override void ExitState()
        {
            _finish = false;
            _active = false;
            
            ClearMove();
            UnsubscribeOnTarget();
        }
        
        private void StartMove()
        {
            _canMove.StartMove(_target.transform, _attackDistance, _speedUpFactor);
            _canMove.OnDestinationTarget.AddListener(TargetDestinationed);
        }
        private void TargetDestinationed()
        {
            StartCoroutine(ActionRoutine());
        }
        private IEnumerator ActionRoutine()
        {
            _actionRun = true;
            yield return new WaitForSeconds(_durationPrepareAction);
           
            if (_target != null && !_target.CheckHaveEffect(_attackType.EffectType))
            {
                var applyEffectProvider = new ApplyEffectProvider();

                if (applyEffectProvider.Provide(_parentEntity, _target, _attackType))
                {
                    OnAttack?.Invoke();
                    _timeAttack = Time.time;
                    _canAttack = false;
                    
                    yield return new WaitForSeconds(_durationAction);
                }
            }
            _actionRun = false;
            _finish = true;
        }
        private void CheckCanAttack()
        {
            if (_actionRun)
                return;
            
            if (_target.CheckHaveEffect(_attackType.EffectType))
                _finish = true;
        }
        private void TargetDestroyed(Entity e)
        { 
            if (_actionRun)
                return;
            
            _finish = true;
        }
        private void ClearMove()
        {
            _canMove.Finish();
            _canMove.OnDestinationTarget.RemoveListener(TargetDestinationed);
        }
        private void FindTarget()
        {
            if (!_canAttack || _target != null)
                return;

            _target = FindTarget(_attackType, _findDistance);

            if (_target == null)
                return;

            SubscribeOnTarget();
        }
        private void SubscribeOnTarget()
        {
            if (_target != null)
            {
                _target.onDeath.AddListener(TargetDestroyed);
                _target.onChangeEffects.AddListener(CheckCanAttack);
            }
        }
        private void UnsubscribeOnTarget()
        {
            if (_target != null)
            {
                _target.onDeath.RemoveListener(TargetDestroyed);
                _target.onChangeEffects.RemoveListener(CheckCanAttack);
                _target = null;
            }
        }
        private Entity FindTarget(AttackType attackType, float dist)
        {  
            var hitColliders = Physics.OverlapSphere(transform.position, dist, _enemiesLayerMask);
            if (hitColliders.Length <= 0) 
                return null;
            
            _findedEntities.Clear();
                
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].transform == transform)
                    continue;
                    
                var entity = hitColliders[i].GetComponent<Entity>();
                if (CanAttack(entity))
                {
                    _findedEntities.Add(entity);
                    return Utility.GetNearEntityMaxDistance(_findedEntities, transform.position, dist);
                }
            }

            return null;

            bool CanAttack(Entity entity)
            {
                return entity != null && !entity.CheckHaveEffect(attackType.EffectType) && entity.EntityType == attackType.EntityType &&
                       entity.EntityType.VulnerableTypes.Contains(attackType);
            }
        }
        #endregion

        #region CALLBAKS     
        // V Code referenced by UnityEvents only V    
        public virtual void SetAttackType(AttackType attackType)
        {
            if (_attackType == attackType)
                return;
            
            _attackType = attackType;

            if (!_actionRun)
                _finish = true;
        }
        #endregion
      
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CollectCreatures
{
    public class AnimationController : MonoBehaviour
    {
        #region VAR
        private Animator _animator;
        private string prevState = "";
        private Vector3 oldPosition;
        private float curSpeed;
        private static readonly int Sleep = Animator.StringToHash("sleep");
        private static readonly int MoveSpeed = Animator.StringToHash("moveSpeed");
        private static readonly int Attack = Animator.StringToHash("attack");
        private static readonly int Evolution = Animator.StringToHash("evolution");
        private static readonly int Eat = Animator.StringToHash("eat");
        private static readonly int Death = Animator.StringToHash("death");
        #endregion

        #region MONO
        private void OnEnable()
        {
            _animator = GetComponentInChildren<Animator>();

            if (_animator == null)
                _animator = GetComponent<Animator>();

            if (_animator == null)
                Debug.Log("Need Animator component");
        }
        private void Update()
        {
            SpeedControl();
        }
        #endregion

        #region FUNC
        private void SpeedControl()
        {
            if (_animator == null)
                return;
            
            curSpeed = Vector3.Distance(oldPosition, transform.position) / Time.deltaTime;
            oldPosition = transform.position;
            
            _animator.SetFloat(MoveSpeed, curSpeed);
        }
        #endregion

        #region CALLBACKS   
        // V Code referenced by UnityEvents only V
        public void StartAttack()
        {
            if (_animator == null)
                return;
            
            if (prevState != "")
                _animator.SetBool(prevState, false);

            _animator.SetTrigger(Attack);
            prevState = "";
        } 
        public void StartEvolution(int lvl)
        {
            if (_animator == null)
                return;
            
            if (prevState != "")
                _animator.SetBool(prevState, false);

            _animator.SetTrigger(Evolution);
            prevState = "";
        }
        public void StartSleep()
        {
            if (_animator == null)
                return;

            if(prevState != "")
                _animator.SetBool(prevState, false);

            _animator.SetBool(Sleep, true);
            prevState = "sleep";
        }
        public void StartEat()
        {
            if (_animator == null)
                return;

            if (prevState != "")
                _animator.SetBool(prevState, false);

            _animator.SetBool(Eat, true);
            prevState = "eat";
        }
        public void StopEat()
        {
            if (_animator == null)
                return;

            if (prevState == "eat")
                _animator.SetBool(Eat, false);
        }
        public void StartDeath(Entity e)
        {
            if (_animator == null)
                return;

            if (prevState != "")
                _animator.SetBool(prevState, false);

            _animator.SetTrigger(Death);
            prevState = "";
        }
        #endregion
    }
}

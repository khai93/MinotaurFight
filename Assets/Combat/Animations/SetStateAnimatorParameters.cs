using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using MinotaurFight.Core;
using System;

namespace MinotaurFight.Combat
{
    [RequireComponent(typeof(StateMachine))]
    [RequireComponent(typeof(Animator))]
    public class SetStateAnimatorParameters : MonoBehaviour
    {
        private StateMachine _stateMachine;
        private Animator _animator;

        private void Awake() 
        {
            _stateMachine = GetComponent<StateMachine>();
            _animator = GetComponent<Animator>();
            _stateMachine.OnStateChanged += OnStateChanged;
        }

        private void OnStateChanged(BaseState _currentState)
        {
            var AvailableStates = _stateMachine.GetAvailableStates().Values.ToList();
            int CurrentStateIndex = AvailableStates.IndexOf(_currentState);

            Debug.Log(CurrentStateIndex);
            _animator.SetInteger("CurrentState", CurrentStateIndex);
            _animator.SetTrigger("ResetState");
        }

        private void OnDestroy()
        {
            _stateMachine.OnStateChanged -= OnStateChanged;
        }
    }
}

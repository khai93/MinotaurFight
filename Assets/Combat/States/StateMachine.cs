using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MinotaurFight.Core;

namespace MinotaurFight.Combat
{
    public class StateMachine : MonoBehaviour
    {
        private Dictionary<Type, BaseState> _availableStates;

        public BaseState CurrentState { get; private set; }
        public event Action<BaseState> OnStateChanged;

        public void SetStates(Dictionary<Type, BaseState> states)
        {
            _availableStates = states;
        }

        private void Update()
        {
            if (CurrentState == null)
            {
                CurrentState = _availableStates.Values.First();
            }

            var nextState = CurrentState?.Tick();

            if (nextState != null &&
                nextState != CurrentState?.GetType())
            {
                SetState(nextState);
            }
        }

        public Dictionary<Type, BaseState> GetAvailableStates()
        {
            return _availableStates;
        }

        private void SetState(Type nextState)
        {
            CurrentState = _availableStates[nextState];
            OnStateChanged?.Invoke(CurrentState);
        }
    }
}

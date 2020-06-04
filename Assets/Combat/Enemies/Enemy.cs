using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MinotaurFight.Core;

namespace MinotaurFight.Combat
{
    [RequireComponent(typeof(StateMachine))]
    public class Enemy : MonoBehaviour, IEnemy
    {

        public Transform Target { get; private set; }

        private void Awake()
        {
            InitializeStateMachine();
        }

        private void InitializeStateMachine()
        {
            var states = new Dictionary<Type, BaseState>() {
                { typeof(IdleState), new IdleState(this) },
                { typeof(ChaseState), new ChaseState(this) }
            };

            GetComponent<StateMachine>().SetStates(states);
        }

        public void SetTarget(Transform target)
        {
            Target = target; 
        }
    }
}

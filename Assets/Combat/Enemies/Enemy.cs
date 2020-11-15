using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MinotaurFight.Core;
using System.Security.Cryptography;

namespace MinotaurFight.Combat
{
    [RequireComponent(typeof(StateMachine))]
    [RequireComponent(typeof(IAttackBehaviour))]
    public class Enemy : MonoBehaviour, IEnemy
    {
        public float MovementSpeed;
        public float Damage;
        public float MinAttackRange;

        public Transform Target { get; private set; }
        public bool IsFacingRight = true;

        public IAttackBehaviour Attack;

        private void Awake()
        {
            InitializeStateMachine();
            Attack = GetComponent<IAttackBehaviour>();
        }

        private void InitializeStateMachine()
        {
            var states = new Dictionary<Type, BaseState>() {
                { typeof(IdleState), new IdleState(this) },
                { typeof(ChaseState), new ChaseState(this) },
                { typeof(AttackState), new AttackState(this) }
            };

            GetComponent<StateMachine>().SetStates(states);
        }

        public void SetTarget(Transform target)
        {
            Target = target; 
        }

        public void DamageTarget()
        {
            var TargetInRange = DistanceFromTarget() < MinAttackRange;

            if (TargetInRange)
            {
                var damage = Target?.GetComponent<IDamageable>();
                damage?.TakeDamage(Damage);
            }
        } 

        public void Flip()
        {
            IsFacingRight = !IsFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        public float DistanceFromTarget()
        {
            return Vector2.Distance(transform.position, Target.position);
        }
    }
}

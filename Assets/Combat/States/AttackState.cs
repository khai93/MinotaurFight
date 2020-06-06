using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MinotaurFight.Core;
using System;

namespace MinotaurFight.Combat
{
    public class AttackState : BaseState
    {
        private Enemy _enemy;

        public AttackState(Enemy enemy) : base(enemy.gameObject)
        {
            _enemy = enemy;
        }

        public override Type Tick()
        {
            if (_enemy.Target == null)
            {
                return typeof(IdleState);
            }

            if (_enemy.Attack.IsStopped())
            {
                _enemy.Attack.Reset();
                return typeof(ChaseState);
            }

            if (!_enemy.Attack.IsAttacking())
            {
                _enemy.Attack.Start();
            }

            return null;
        }
    }
}


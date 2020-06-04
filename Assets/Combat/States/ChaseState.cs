using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MinotaurFight.Core;
using System;

namespace MinotaurFight.Combat
{
    public class ChaseState : BaseState
    {

        private Enemy _enemy;

        public ChaseState(Enemy enemy) : base(enemy.gameObject)
        {
            _enemy = enemy;
        }

        public override Type Tick()
        {
            if (_enemy.Target == null)
            {
                return typeof(IdleState);
            }

            _enemy.transform.position = Vector2.MoveTowards(_enemy.transform.position, _enemy.Target.position, _enemy.MovementSpeed * Time.deltaTime);

            if (_enemy.Target.position.x > _enemy.transform.position.x && !_enemy.IsFacingRight) 
                _enemy.Flip();
            if (_enemy.Target.position.x < _enemy.transform.position.x && _enemy.IsFacingRight)
                _enemy.Flip();

            if (_enemy.DistanceFromTarget() < _enemy.MinAttackRange)
            {
                return typeof(AttackState);
            }

            return null;
        }
    }
}


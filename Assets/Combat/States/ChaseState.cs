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

            return null;
        }
    }
}


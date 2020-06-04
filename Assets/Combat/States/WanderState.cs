using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MinotaurFight.Core;
using System;

namespace MinotaurFight.Combat 
{
    public class WanderState : BaseState
    {

        private Enemy _enemy;

        public WanderState(Enemy enemy) : base(enemy.gameObject)
        {
            _enemy = enemy;
        }

        public override Type Tick()
        {
            if (_enemy.Target != null)
            {
                return typeof(ChaseState);
            }

            return null;
        }
    }
}


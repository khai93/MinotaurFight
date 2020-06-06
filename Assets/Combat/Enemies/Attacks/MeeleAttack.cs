using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MinotaurFight.Core;

namespace MinotaurFight.Combat
{
    public class MeeleAttack : MonoBehaviour, IAttackBehaviour
    {
        private bool _attacking = false;
        public bool IsAttacking() => _attacking;

        private bool _stopped = false;
        public bool IsStopped() => _stopped;

        public void Reset() {
            _attacking = false;
            _stopped = false;
        }

        public void Start()
        {
            _attacking = true;
            _stopped = false;
        }

        public void Stop()
        {
            _attacking = false;
            _stopped = true;
        }
    }
}

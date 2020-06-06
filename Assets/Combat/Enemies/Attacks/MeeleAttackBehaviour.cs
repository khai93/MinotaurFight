using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MinotaurFight.Core;

namespace MinotaurFight.Combat
{
    public class MeeleAttackBehaviour : MonoBehaviour, IAttackBehaviour
    {
        private bool _attacking = false;
        public bool IsAttacking() => _attacking;

        private bool _stopped = false;
        public bool IsStopped() => _stopped;

        public void ResetAttack() {
            _attacking = false;
            _stopped = false;
        }

        public void StartAttack()
        {
            _attacking = true;
            _stopped = false;
        }

        public void StopAttack()
        {
            _attacking = false;
            _stopped = true;
        }
    }
}

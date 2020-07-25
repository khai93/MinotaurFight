using UnityEngine;
using MinotaurFight.Core;

namespace MinotaurFight.Combat
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Renderer))]
    [RequireComponent(typeof(IEnemy))]
    public class TargetOnHit : MonoBehaviour
    {
        public float MinChaseRange;
        public float MaxChaseRange;

        private Health _health;
        private IEnemy _target;

        private bool _damaged;
        private bool _targeted;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _target = GetComponent<IEnemy>();

            _health.Damaged += OnDamaged;
        }


        private void Update()
        {
            if (_damaged == true) {
                var closest = FindClosestPlayer();

                _target.SetTarget(target: closest);
            }
        }

        private void OnDamaged()
        {
            _damaged = true;
        }

        private void OnDestroy()
        {
            _health.Damaged -= OnDamaged;
        }

        public Transform FindClosestPlayer()
        {
            Transform closest = null;
            float closestDistance = Mathf.Infinity;

            foreach (Transform player in PlayersContainer.Instance.Players)
            {
                float distance = Vector2.Distance(transform.position, player.position);

                if (distance < closestDistance && player.gameObject.activeSelf)
                {
                    closestDistance = distance;
                    closest = player;
                }
            }

            var IsOutOfRangeWhileTargeted = (_targeted == true && closestDistance > MaxChaseRange);
            var IsOutOfRange = (!_targeted && closestDistance > MinChaseRange);

            if (IsOutOfRangeWhileTargeted || IsOutOfRange)
            {
                _targeted = false;
                return null;
            }

            _targeted = true;
            return closest;
        }
    }
}
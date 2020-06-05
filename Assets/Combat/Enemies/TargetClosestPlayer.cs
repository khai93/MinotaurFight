using MinotaurFight.Core;
using UnityEngine;

namespace MinotaurFight.Combat
{
    [RequireComponent(typeof(IEnemy))]
    public class TargetClosestPlayer : MonoBehaviour
    {
        public float MinChaseRange;
        public float MaxChaseRange;

        private bool _targeted;

        private IEnemy _target;

        private void Awake()
        {
            _target = GetComponent<IEnemy>();
        }

        private void Update()
        {
            var closest = FindClosest();

            _target.SetTarget(target: closest);
        }

        private Transform FindClosest()
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
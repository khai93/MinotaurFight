using MinotaurFight.Core;
using UnityEngine;

namespace MinotaurFight.Combat
{
    public class DamageOnTouch : MonoBehaviour, IMultiplier
    {
        [SerializeField]
        private GameObject effectPrefab;

        [SerializeField]
        private float Damage;

        [SerializeField]
        private float DamageMultiplier;

        [SerializeField]
        private bool DestroyOnTouch;

        [SerializeField]
        private string EnemyTag;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(EnemyTag))
            {
                TryShowEffect();
                DealDamage(collision);

                if (DestroyOnTouch)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void TryShowEffect()
        {
            if (effectPrefab != null)
            {
                var effect = Instantiate(effectPrefab);
                effect.transform.position = transform.position;
            }

        }

        public void SetMultiplier(float multiplier)
        {
            DamageMultiplier = multiplier;
        }

        public void SetDamage(float damage)
        {
            Damage = damage;
        }

        private void DealDamage(Collider2D collision)
        {
            IDamageable _takeDamage = collision.GetComponent<IDamageable>();
            _takeDamage.TakeDamage(Damage * DamageMultiplier);
        }
    }
}
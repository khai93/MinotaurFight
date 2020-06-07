using MinotaurFight.Core;
using System;
using UnityEngine;

namespace MinotaurFight.Combat
{
    public class Health : MonoBehaviour, IDamageable
    {
        public float MaxHealth;

        public event Action Damaged;

        [SerializeField]
        private float HealthRegenRate;

        [SerializeField]
        private GameObject DeathEffectPrefab;

        [SerializeField]
        private bool DestroyOnDeath;

        private float HealthRegenCD;
        private float _health;
        private bool _canTakeDamage = true;

        private SpriteRenderer _renderer;

        public void TakeDamage(float damage)
        {
            if (_canTakeDamage)
            {
                _health -= damage;
                Damaged?.Invoke();
            }
        }

        private void Awake()
        {
            _health = MaxHealth;
        }

        private void Update()
        {
            // Death Check
            if (_health <= 0)
            {
                Die();
            }

            // Health Regen
            if (Time.time >= HealthRegenCD && _health < MaxHealth)
            {
                _health += 1;
                HealthRegenCD = Time.time + HealthRegenRate;
            }
        }

        public float GetCurrent()
        {
            return _health;
        }

        public void SetHealthPercentage(float percentage)
        {
            _health = MaxHealth * (percentage / 100);
        }

        public void SetDamageableStatus(bool status)
        {
            _canTakeDamage = status;
        }

        private void Die()
        {
            if (DeathEffectPrefab != null)
            {
                var effect = Instantiate(DeathEffectPrefab);
                effect.transform.position = transform.position;
            }

            if (DestroyOnDeath)
            {
                Destroy(gameObject);
            }

            gameObject.SetActive(false);
        }
    }
}
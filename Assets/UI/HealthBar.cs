using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MinotaurFight.Combat;

namespace MinotaurFight.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;

        [SerializeField]
        private Image fill;

        [SerializeField]
        private Health health;

        [SerializeField]
        private Gradient gradient;

        [SerializeField]
        private Camera cam;

        [SerializeField]
        private Transform target;

        [SerializeField]
        private float posOffset;

        private void Awake()
        {
            slider.maxValue = health.MaxHealth;
            slider.value = health.MaxHealth;
            health.Damaged += OnDamaged;
            fill.color = gradient.Evaluate(1f);
        }

        private void Update()
        {
            slider.transform.position = cam.WorldToScreenPoint(target.position + new Vector3(0, posOffset, 0));
        }

        private void OnDamaged()
        {
            float currentHP = health.GetCurrent();
            slider.value = currentHP;
            fill.color = gradient.Evaluate(currentHP/health.MaxHealth);
        }

        private void OnDestroy()
        {
            health.Damaged -= OnDamaged;
        }
    }
}
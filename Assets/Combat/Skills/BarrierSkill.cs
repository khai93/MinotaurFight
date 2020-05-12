using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MinotaurFight.Core;

namespace MinotaurFight.Combat
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class BarrierSkill : MonoBehaviour, ISkill
    {

        [SerializeField]
        private GameObject EffectPrefab;

        [SerializeField]
        private float Duration;

        [SerializeField]
        private float Cooldown;

        public bool _isSkillActive;

        private Animator _anim;
        private Health _health;
        private Rigidbody2D _rb;
        private SpriteRenderer _spr;
        private float _cd;
        private float _skillInactiveTime;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _health = GetComponent<Health>();
            _rb = GetComponent<Rigidbody2D>();
            _spr = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (Time.time >= _skillInactiveTime)
            {
                _isSkillActive = false;
                _health.SetDamageableStatus(true);
            }
        }

        public void ExecuteSkill(bool isKeyDown)
        {
            var canCastSkill = Time.time >= _cd;

            if (canCastSkill)
            {
                 if (!_isSkillActive && isKeyDown)
                {

                    _isSkillActive = true;

                    var effectPrefab = TrySpawnEffect();

                    if (effectPrefab)
                    {
                        effectPrefab.transform.localScale = transform.localScale;

                        var _destroy = effectPrefab.GetComponent<DestroyAfterLifetime>();
                        _destroy.SetLifetime(Duration);
                    }

                    _health.SetDamageableStatus(false);

                    _cd = Time.time + Cooldown;
                    _skillInactiveTime = Time.time + Duration;
                }
            }
        }

        private GameObject TrySpawnEffect()
        {
            var effectPrefab = Instantiate(EffectPrefab);
            effectPrefab.transform.position = transform.position;
            effectPrefab.transform.SetParent(transform);

            return effectPrefab;
        }

        public bool IsActive() => _isSkillActive;
    }
}

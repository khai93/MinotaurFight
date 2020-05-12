using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MinotaurFight.Core;
using System.Threading;

namespace MinotaurFight.Combat
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerJump))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ShootProjectileUltimate : MonoBehaviour, ISkill
    {
        [SerializeField]
        private Transform FirePoint;

        [SerializeField]
        private GameObject ProjectilePrefab;

        [SerializeField]
        private float Damage;

        [SerializeField]
        private float Cooldown;

        public bool _isSkillActive;

        private Animator _anim;
        private PlayerJump _plyJump;
        private Rigidbody2D _rb;
        private SpriteRenderer _spr;
        private float _cd;

        private float _startChargeTime;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _plyJump = GetComponent<PlayerJump>();
            _rb = GetComponent<Rigidbody2D>();
            _spr = GetComponent<SpriteRenderer>();
        }

        public void ExecuteSkill(bool isKeyDown)
        {
            var canCastSkill = Time.time >= _cd;

            if (canCastSkill && _plyJump.IsGrounded)
            {
                if (!_isSkillActive && isKeyDown)
                {
                    _isSkillActive = true;
                    
                    _rb.constraints = _rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    _anim.SetTrigger("Ult");
                }
            }
        }

        public void SpawnUltProjectile()
        {
            var projectilePrefab = TrySpawnProjectile();

            if (projectilePrefab)
            {
                var damage = projectilePrefab.GetComponent<DamageOnTouch>();
                damage.SetDamage(Damage);
            }

            _rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            _isSkillActive = false;
            _cd = Time.time + Cooldown;
        }

        private GameObject TrySpawnProjectile()
        {
            var projectilePrefab = Instantiate(ProjectilePrefab);

            Vector3 flippedFirepoint = FirePoint.position - new Vector3(0.59f * 2, 0);

            if (_spr.flipX)
            {
                projectilePrefab.transform.position = flippedFirepoint;
                projectilePrefab.transform.right = projectilePrefab.transform.right * -1;
            }
            else
            {
                projectilePrefab.transform.position = FirePoint.position;
            }

            return projectilePrefab;
        }

        public bool IsActive() => _isSkillActive;
    }
}

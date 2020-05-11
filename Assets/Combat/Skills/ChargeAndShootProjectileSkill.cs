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
    public class ChargeAndShootProjectileSkill : MonoBehaviour, ISkill
    {
        [SerializeField]
        private Transform FirePoint;

        [SerializeField]
        private GameObject ProjectilePrefab;

        [SerializeField]
        private float ChargeRate;

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
                if (_isSkillActive && !isKeyDown)
                {
                    _isSkillActive = false;
                    _rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                    _anim.SetTrigger("Shoot");

                    var projectilePrefab = TrySpawnProjectile();

                    float chargeTimePassedHalved = (Time.time - _startChargeTime)/2 + 0.25f;
                    float chargedAmount = chargeTimePassedHalved * ChargeRate;
                    float charged = chargedAmount < 10 ? chargedAmount : 10;

                    if (projectilePrefab)
                    {
                        projectilePrefab.transform.localScale = new Vector3(charged, charged, charged);

                        IMultiplier multiplier = projectilePrefab.GetComponent<IMultiplier>();
                        multiplier.SetMultiplier(charged*3);
                    }


                    _cd = Time.time + Cooldown;
                }
                else if (!_isSkillActive && isKeyDown)
                {

                    _isSkillActive = true;
                    _rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    _anim.SetTrigger("StartCharge");
                    _startChargeTime = Time.time;
                }
            }
        }

        private GameObject TrySpawnProjectile()
        {
            var projectilePrefab = Instantiate(ProjectilePrefab);

            Vector3 flippedFirepoint = FirePoint.position - new Vector3(0.59f * 2, 0);

            if (_spr.flipX)
            {
                projectilePrefab.transform.position = flippedFirepoint;
                projectilePrefab.transform.right = projectilePrefab.transform.right * -1;
            } else
            {
                projectilePrefab.transform.position = FirePoint.position;
            }

            return projectilePrefab;
        }

        public bool IsActive() => _isSkillActive;
    }
}

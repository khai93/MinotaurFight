using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MinotaurFight.Core;
using System.Security.Cryptography;

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

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _plyJump = GetComponent<PlayerJump>();
            _rb = GetComponent<Rigidbody2D>();
            _spr = GetComponent<SpriteRenderer>();
        }

        public void ExecuteSkill(bool isKeyDown)
        {
            // Debug.Log("Executed: " + isKeyDown);
            if (_isSkillActive)
            {
                _isSkillActive = false;
                _rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                _anim.SetTrigger("Shoot");
                TrySpawnProjectile();
            } else
            {
                if (_plyJump.IsGrounded)
                {
                    _isSkillActive = true;
                    _rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    _anim.SetTrigger("StartCharge");
                }
            }
        }

        private void TrySpawnProjectile()
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
        }
    }
}

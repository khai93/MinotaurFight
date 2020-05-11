using UnityEngine;
using MinotaurFight.Core;

namespace MinotaurFight.Combat
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class EntityKnockback : MonoBehaviour, IKnockbackable
    {
        private Rigidbody2D _rb;
        private SpriteRenderer _spr;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _spr = GetComponent<SpriteRenderer>();
        }

        public void Knockback(float force)
        {
            float scalar = (_spr.flipX ? 1 : -1);
            Debug.Log("hello");
            _rb.AddForce(transform.right * scalar * (force * 25), ForceMode2D.Force);
        }
    }
}
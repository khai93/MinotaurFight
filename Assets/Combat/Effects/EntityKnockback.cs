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
            _rb.AddForce(transform.right * -1 * (force * 250), ForceMode2D.Force);
        }
    }
}
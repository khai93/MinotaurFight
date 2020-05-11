using UnityEngine;
using MinotaurFight.Core;

namespace MinotaurFight.Combat
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(KeyboardKeyBinder))]
    [RequireComponent(typeof(EntityKnockback))]
    [RequireComponent(typeof(Animator))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float WalkSpeed;

        [SerializeField]
        private float DoubleTapMaxTime;

        private Direction _Facing = Direction.Right;
        private float _scalar = 0;
        private Direction _lastTapDirection;
        private float _lastTapTime;

        private Rigidbody2D _rb;
        private SpriteRenderer _sr;
        private KeyboardKeyBinder _keyBinder;
        private Animator _anim;
        private EntityKnockback _knockback;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
            _keyBinder = GetComponent<KeyboardKeyBinder>();
            _anim = GetComponent<Animator>();
            _knockback = GetComponent<EntityKnockback>();
        }

        private void Flip()
        {
            bool facingRight = _Facing == Direction.Right;
            _Facing = facingRight ? Direction.Left : Direction.Right;
            _sr.flipX = facingRight;
        }

        private void FixedUpdate()
        {
            _rb.velocity = new Vector2(WalkSpeed * _scalar * 10 * Time.deltaTime, _rb.velocity.y);
        }

        public void Move(Direction dirPressed)
        {
            float scalar = 0;

            if (dirPressed == Direction.Left)
            {
                // Flip if changed direction while moving
                if (_Facing == Direction.Right)
                    Flip();

                scalar -= 1;
                _lastTapDirection = Direction.Left;

                var lastTapInRange = (Time.time - _lastTapTime) < DoubleTapMaxTime;
                if (_lastTapDirection == Direction.Left && lastTapInRange)
                {
                    _knockback.Knockback(5);
                } else
                {
                    _lastTapTime = Time.time;
                }
            }
            else if (dirPressed == Direction.Right)
            {
                if (_Facing == Direction.Left)
                    Flip();

                scalar += 1;
                _lastTapDirection = Direction.Right;

                var lastTapInRange = (Time.time - _lastTapTime) < DoubleTapMaxTime;
                if (_lastTapDirection == Direction.Right && lastTapInRange)
                {
                    _knockback.Knockback(-5);
                }
                else
                {
                    _lastTapTime = Time.time;
                }
            }

            _anim.SetInteger("MovementScalar", (int) scalar);

            _scalar = scalar;
        }

        public void ResetScalar()
        {
            _anim.SetInteger("MovementScalar", 0);
            _scalar = 0;
        }
    }
}

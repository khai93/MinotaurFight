using UnityEngine;
using MinotaurFight.Core;

namespace MinotaurFight.Combat
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(KeyboardKeyBinder))]
    [RequireComponent(typeof(EntityKnockback))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float WalkSpeed;

        [SerializeField]
        private float MaxDashes;

        [SerializeField]
        private float DashRecoveryTime;

        [SerializeField]
        private float DoubleTapMaxTime;

        private Direction _Facing = Direction.Right;
        private float _scalar = 0;
        private Direction _lastTapDirection;
        private float _lastTapTime;
        private float _nextRecoveryTime;
        private float _dashesLeft;

        private Rigidbody2D _rb;
        private SpriteRenderer _sr;
        private KeyboardKeyBinder _keyBinder;
        private Animator _anim;
        private EntityKnockback _knockback;
        private BoxCollider2D _collide;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
            _keyBinder = GetComponent<KeyboardKeyBinder>();
            _anim = GetComponent<Animator>();
            _knockback = GetComponent<EntityKnockback>();
            _collide = GetComponent<BoxCollider2D>();
            _dashesLeft = MaxDashes;
        }

        private void Flip()
        {
            bool facingRight = _Facing == Direction.Right;
            _Facing = facingRight ? Direction.Left : Direction.Right;
            _sr.flipX = facingRight;
        }

        private void FixedUpdate()
        {
            CheckDashes();
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
                
                var lastTapInRange = (Time.time - _lastTapTime) < DoubleTapMaxTime;
                if (_lastTapDirection == Direction.Left && lastTapInRange && _dashesLeft > 0)
                {
                    _dashesLeft--;
                    _collide.enabled = false;
                    _knockback.Knockback(50);
                    _collide.enabled = true;
                } else 
                {
                    _lastTapTime = Time.time;
                }

                _lastTapDirection = Direction.Left;
            }
            else if (dirPressed == Direction.Right)
            {
                if (_Facing == Direction.Left)
                    Flip();

                scalar += 1;
                
                var lastTapInRange = (Time.time - _lastTapTime) < DoubleTapMaxTime;
                if (_lastTapDirection == Direction.Right && lastTapInRange && _dashesLeft > 0)
                {
                    _dashesLeft--;
                    
                    _knockback.Knockback(-50);
                   
                } else
                {
                    _lastTapTime = Time.time;
                }
                _lastTapDirection = Direction.Right;
            }

            _anim.SetInteger("MovementScalar", (int) scalar);

            _scalar = scalar;
        }

        private void CheckDashes()
        {
            if (_dashesLeft < MaxDashes && Mathf.Approximately(_nextRecoveryTime, 0f))
            {
                _nextRecoveryTime = Time.time + DashRecoveryTime;
                Debug.Log("Recovery Time Started");
            }

            if (Time.time >= _nextRecoveryTime && !Mathf.Approximately(_nextRecoveryTime, 0f))
            {
                _nextRecoveryTime = 0;
                _dashesLeft++;
                Debug.Log("Dash Replinished");
            }
        }

        public void ResetScalar()
        {
            _anim.SetInteger("MovementScalar", 0);
            _scalar = 0;
        }
    }
}

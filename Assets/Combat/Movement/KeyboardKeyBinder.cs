using UnityEngine;

namespace MinotaurBattle.Combat
{
    [RequireComponent(typeof(PlayerJump))]
    [RequireComponent(typeof(PlayerMovement))]
    public class KeyboardKeyBinder : MonoBehaviour
    {
        // Keys
        [SerializeField]
        private KeyCode LeftKey;
        [SerializeField]
        private KeyCode RightKey;
        [SerializeField]
        private KeyCode JumpKey;
        [SerializeField]
        private KeyCode Skill1Key;
        [SerializeField]
        private KeyCode Skill2Key;
        [SerializeField]
        private KeyCode Skill3Key;

        // Required Behavior Components
        private PlayerJump _playerJump;
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerJump = GetComponent<PlayerJump>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            CheckMovementInputs();
            CheckJumpInputs();
        }

        private void CheckJumpInputs()
        {
            if (Input.GetKeyDown(JumpKey))
            {
                _playerJump.TryToJump();
            }
        }

        private void CheckMovementInputs()
        {
            if (Input.GetKeyDown(LeftKey))
            {
                _playerMovement.Move(Direction.Left);
            }

            if (Input.GetKeyDown(RightKey))
            {
                _playerMovement.Move(Direction.Right);
            }

            // Reset Scalar if player is not moving
            if (!Input.GetKey(LeftKey) && !Input.GetKey(RightKey))
            {
                _playerMovement.ResetScalar();
            }
        }
    }
}
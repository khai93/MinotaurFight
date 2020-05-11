using UnityEngine;
using MinotaurFight.Core;

namespace MinotaurFight.Combat
{
    [RequireComponent(typeof(PlayerJump))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(SkillsManager))]
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
        private SkillsManager _skillsManager;

        private KeyCode[] _skillKeys = new KeyCode[3];

        private void Awake()
        {
            _playerJump = GetComponent<PlayerJump>();
            _playerMovement = GetComponent<PlayerMovement>();
            _skillsManager = GetComponent<SkillsManager>();

            _skillKeys[0] = Skill1Key;
            _skillKeys[1] = Skill2Key;
            _skillKeys[2] = Skill3Key; 
        }

        private void Update()
        {
            CheckMovementInputs();
            CheckJumpInputs();
            CheckSkillInputs();
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

        private void CheckSkillInputs()
        {
            for(int i = 0;i < _skillKeys.Length;i++)
            {
                if (Input.GetKeyDown(_skillKeys[i]))
                {
                    _skillsManager.skills[i].ExecuteSkill(true);
                }

                if (Input.GetKeyUp(_skillKeys[i]))
                {
                    _skillsManager.skills[i].ExecuteSkill(false);
                }
            }
        }
    }
}
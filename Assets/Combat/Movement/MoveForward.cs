using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinotaurFight.Combat
{
    public class MoveForward : MonoBehaviour
    {
        [SerializeField]
        private float Speed;

        private void Update()
        {
            transform.position += transform.right * Speed * Time.deltaTime;
        }
    }
}

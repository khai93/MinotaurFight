using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinotaurFight.Combat
{
    public class DestroyAfterLifetime : MonoBehaviour
    {
        [SerializeField]
        private float LifeTime;

        private float _deathTime;

        private void Start()
        {
            _deathTime = Time.time + LifeTime;
        }

        private void Update()
        {
            if (Time.time >= _deathTime)
            {
                Destroy(gameObject);
            }
        }

        public void SetLifetime(float lifetime)
        {
            LifeTime = lifetime;
            _deathTime = Time.time + LifeTime;
        }
    }
}

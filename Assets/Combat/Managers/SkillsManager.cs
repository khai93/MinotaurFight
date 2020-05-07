using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MinotaurFight.Core
{
    public class SkillsManager : MonoBehaviour
    {
        public ISkill[] skills = new ISkill[2];

        private void Awake()
        {
            int i = 0;
            foreach (var skill in GetComponentsInChildren<Transform>(true).Where(t => t is ISkill).Cast<ISkill>()) {
                skills[i] = skill;
                i++;
            }

        }

        private void Update()
        {

        }
    }
}

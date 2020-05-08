using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MinotaurFight.Combat;

namespace MinotaurFight.Core
{
    public class SkillsManager : MonoBehaviour
    {
        public ISkill[] skills = new ISkill[3];

        private void Awake()
        {
            ISkill[] _ComponentSkills = GetComponents<ISkill>();

            int i = 0;
            foreach (var skill in _ComponentSkills) {
                skills[i] = skill;
                i++;
            }

        }
    }
}

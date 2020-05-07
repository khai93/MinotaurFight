using UnityEngine;
using MinotaurFight.Core;

namespace MinotaurFight.Combat
{
    [System.Serializable]
    public abstract class Skill
    {
        public abstract void ExecuteSkill();
    }
}

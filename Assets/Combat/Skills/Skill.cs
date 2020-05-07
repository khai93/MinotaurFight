using UnityEngine;

namespace MinotaurFight.Combat
{
    public abstract class Skill : ISkill
    {
        public abstract void ExecuteSkill(){ }
    }
}

namespace MinotaurFight.Core
{
    public interface IAttackBehaviour
    {
        void StartAttack();
        void StopAttack();
        void ResetAttack();
        bool IsAttacking();
        bool IsStopped();
    }
}
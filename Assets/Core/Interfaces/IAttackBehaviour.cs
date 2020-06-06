namespace MinotaurFight.Core
{
    public interface IAttackBehaviour
    {
        void Start();
        void Stop();
        void Reset();
        bool IsAttacking();
        bool IsStopped();
    }
}
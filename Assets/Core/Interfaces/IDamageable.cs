namespace MinotaurFight.Core
{
    public interface IDamageable
    {
        void TakeDamage(float damage);
        void SetDamageableStatus(bool status);
    }
}

namespace MinotaurFight.Core
{
    public interface ISkill
    {
        void ExecuteSkill(bool isKeyDown);
        bool IsActive();
    }
}

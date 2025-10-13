namespace LonecraftGames.Toolkit.Gameplay.HealthPro
{
    public interface IHealer
    {
        int HealAmount { get; }
        HealType Type { get; }
        void Heal(IHealth target);
        
    }
}
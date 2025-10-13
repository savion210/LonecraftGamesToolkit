namespace LonecraftGames.Toolkit.Gameplay.HealthPro
{
    /// <summary>
    ///  Interface for objects that have health.
    /// </summary>
    public interface IHealth
    {
        int MaxHealth { get; }
        int CurrentHealth { get; }
        void TakeDamage(int damage, DamageType type);
        void Heal(int amount);
        bool IsDead { get; }
    }

}
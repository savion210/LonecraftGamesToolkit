namespace LonecraftGames.Toolkit.Gameplay.HealthPro
{
    /// <summary>
    ///  Interface for objects that can deal damage.
    /// </summary>
    public interface IDamageDealer
    {
        int Damage { get; }
        DamageType Type { get; }
        void DealDamage(IHealth target);
    }

}


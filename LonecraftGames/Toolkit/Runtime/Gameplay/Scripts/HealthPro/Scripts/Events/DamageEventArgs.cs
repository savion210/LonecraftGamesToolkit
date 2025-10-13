using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.HealthPro
{
    /// <summary>
    ///   Event arguments for damage events.
    /// </summary>
    public class DamageEventArgs
    {
        public MonoBehaviour Dealer { get; }
        public IHealth Target { get; }
        public int Damage { get; }
        public DamageType Type { get; }

        public DamageEventArgs(MonoBehaviour dealer, IHealth target, int damage, DamageType type)
        {
            Dealer = dealer;
            Target = target;
            Damage = damage;
            Type = type;
        }
    }

}
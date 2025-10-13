using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.HealthPro
{
    /// <summary>
    ///   Base class for all damage dealers.
    /// </summary>
    [AddComponentMenu("LonecraftGames/HealthPro/BaseDamageDealer")]
    public abstract class BaseDamageDealer : MonoBehaviour, IDamageDealer
    {
        [SerializeField] protected int damage = 10;
        [SerializeField] protected DamageType damageType = DamageType.Physical;
        [SerializeField] protected OnDamageEventChannel onDamageDealtEvent;

        public int Damage => damage;
        public DamageType Type => damageType;

        /// <summary>
        ///  Deal damage to the target.
        /// </summary>
        /// <param name="target">The target to deal damage to.</param>
        public abstract void DealDamage(IHealth target);

        /// <summary>
        ///  Raise the damage event.
        /// </summary>
        /// <param name="target"> The target to deal damage to.</param>
        /// <param name="damageAmount"> The amount of damage to deal.</param>
        protected void RaiseDamageEvent(IHealth target, int damageAmount)
        {
            onDamageDealtEvent.Raise(new DamageEventArgs(this, target, damageAmount, damageType));
        }
    }
}
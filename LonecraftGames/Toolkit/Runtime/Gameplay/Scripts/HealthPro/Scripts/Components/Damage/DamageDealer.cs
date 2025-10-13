using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.HealthPro
{
    /// <summary>
    ///   Deals damage to a health component.
    /// </summary>
    [AddComponentMenu("LonecraftGames/HealthPro/DamageDealer")]
    public class DamageDealer : BaseDamageDealer
    {
        
        /// <summary>
        ///  Deals damage to the target health component.
        /// </summary>
        /// <param name="target">The health component to deal damage to.</param>
        public override void DealDamage(IHealth target)
        {
            if (target != null)
            {
                target.TakeDamage(damage, damageType);
                //RaiseDamageEvent(target, damage);
            }
        }
    }
}
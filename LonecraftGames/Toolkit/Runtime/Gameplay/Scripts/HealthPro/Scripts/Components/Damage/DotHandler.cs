using System.Collections;
using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.HealthPro
{
    /// <summary>
    ///  Deals damage over time to a health component.
    /// </summary>
    [AddComponentMenu("LonecraftGames/HealthPro/DotHandler")]
    public class DotHandler : BaseDamageDealer
    {
        [SerializeField] private int damagePerTick = 5;
        [SerializeField] private float tickInterval = 1f;
        [SerializeField] private float duration = 5f;

        /// <summary>
        ///  Deals damage to the target health component.
        /// </summary>
        /// <param name="target">The health component to deal damage to.</param>
        public override void DealDamage(IHealth target)
        {
            if (target != null)
            {
                StartCoroutine(DoDamageOverTime(target));
            }
        }

        /// <summary>
        ///  Deals damage over time to the target health component.
        /// </summary>
        /// <param name="target">The health component to deal damage to.</param>
        /// <returns>An IEnumerator for the coroutine.</returns>
        private IEnumerator DoDamageOverTime(IHealth target)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                if (target.IsDead) yield break;

                target.TakeDamage(damagePerTick, damageType);
                elapsed += tickInterval;
                RaiseDamageEvent(target, damagePerTick);
                yield return new WaitForSeconds(tickInterval);
            }
        }
    }
}
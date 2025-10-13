using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.HealthPro
{
    /// <summary>
    ///   A collider that deals damage to any health component it collides with.
    /// </summary>
    [AddComponentMenu("LonecraftGames/HealthPro/DamageCollider")]
    public class DamageCollider : MonoBehaviour
    {
        [SerializeField] private BaseDamageDealer damageDealer;

        private void OnTriggerEnter(Collider other)
        {
            var targetHealth = other.GetComponent<IHealth>();
            if (targetHealth != null)
            {
                damageDealer.DealDamage(targetHealth);
            }
        }
    }
}
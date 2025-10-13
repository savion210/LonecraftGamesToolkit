using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.HealthPro
{
    [AddComponentMenu("LonecraftGames/HealthPro/HealCollider")]
    public class HealCollider : MonoBehaviour
    {
        [SerializeField] private BaseHealer healer;

        private void OnTriggerEnter(Collider other)
        {
            var targetHealth = other.GetComponent<IHealth>();
            if (targetHealth != null)
            {
                healer.Heal(targetHealth);
            }
        }
    }
}
using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.HealthPro
{
    /// <summary>
    ///   A simple UI health bar that listens to health change events and updates the UI accordingly.
    /// </summary>
    [AddComponentMenu("LonecraftGames/HealthPro/UIHealthBar")]
    public class UIHealthBar : MonoBehaviour
    {
        [Tooltip("The target entity to listen for health changes.")]
        [Header("Settings")]
        [SerializeField] private MonoBehaviour targetEntity;
        
        [Tooltip("The health event channel to listen for health changes.")]
        [Header("Events")]
        [SerializeField] private OnHealthChangedEventChannel onHealthChangedEvent;

        private void OnEnable()
        {
            onHealthChangedEvent.RegisterListener(UpdateHealthBar);
        }

        private void OnDisable()
        {
            onHealthChangedEvent.UnregisterListener(UpdateHealthBar);
        }

        /// <summary>
        ///  Updates the health bar UI with the new health value.
        /// </summary>
        /// <param name="args"> The health event arguments. </param>
        private void UpdateHealthBar(HealthEventArgs args)
        {
            if (args.Entity != targetEntity) return; // Ignore events not related to this entity
            UpdateHealthUI(args.CurrentHealth);
        }

        /// <summary>
        ///  Updates the health bar UI with the new health value.
        /// </summary>
        /// <param name="currentHealth"> The current health value. </param>
        private void UpdateHealthUI(int currentHealth)
        {
            // Update the UI with the new health value
        }
    }
}
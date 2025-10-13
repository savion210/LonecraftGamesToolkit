using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.HealthPro
{
    /// <summary>
    ///  Event arguments for health events.
    /// </summary>
    public class HealthEventArgs
    {
        public MonoBehaviour Entity { get; }
        public int CurrentHealth { get; }

        public HealthEventArgs(MonoBehaviour entity, int currentHealth)
        {
            Entity = entity;
            CurrentHealth = currentHealth;
        }
    }

}
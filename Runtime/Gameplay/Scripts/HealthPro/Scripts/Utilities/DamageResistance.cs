using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.HealthPro
{
    /// <summary>
    ///   Represents a damage resistance for a specific damage type.
    /// </summary>
    [System.Serializable]
    public class DamageResistance
    {
        public DamageType Type;
        [Range(0f, 1f)]
        public float ResistancePercentage; // e.g., 0.2f for 20% resistance
    }
}
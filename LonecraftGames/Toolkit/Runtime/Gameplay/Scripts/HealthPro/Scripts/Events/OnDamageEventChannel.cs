using LonecraftGames.Toolkit.Core.Events;
using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.HealthPro
{
    /// <summary>
    ///  Event channel for damage events.
    /// </summary>
    [CreateAssetMenu (fileName = "OnDamageEventChannel", menuName = "LonecraftGames/Events/HealthPro/OnDamageEventChannel")]
    public class OnDamageEventChannel : EventChannel<DamageEventArgs>{ }
}
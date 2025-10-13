using LonecraftGames.Toolkit.Core.Events;
using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.HealthPro
{
    /// <summary>
    ///  Event channel for death events.
    /// </summary>
    [CreateAssetMenu(fileName = "OnDeathEvent", menuName = "LonecraftGames/Events/HealthPro/OnDeathEventChannel")]
    public class OnDeathEventChannel : EventChannel<MonoBehaviour> { }
}
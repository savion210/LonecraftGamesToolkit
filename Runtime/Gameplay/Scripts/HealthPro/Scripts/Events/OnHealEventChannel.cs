using LonecraftGames.Toolkit.Core.Events;
using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.HealthPro
{
    [CreateAssetMenu(fileName = "OnHealEventChannel", menuName = "LonecraftGames/Events/HealthPro/OnHealEventChannel")]
    public class OnHealEventChannel : EventChannel<HealEventArgs> { }
}
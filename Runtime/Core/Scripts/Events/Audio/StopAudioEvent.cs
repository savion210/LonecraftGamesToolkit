using UnityEngine;

namespace LonecraftGames.Toolkit.Core.Events
{
    [CreateAssetMenu(fileName = "StopAudioEvent", menuName = "LonecraftGames/Events/Audio/StopAudioEvent")]
    public class StopAudioEvent : EventChannel<string>
    {
    }
}
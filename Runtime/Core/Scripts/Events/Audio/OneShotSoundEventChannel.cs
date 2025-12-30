using LonecraftGames.Toolkit.Core.Data;
using LonecraftGames.Toolkit.Core.Events;
using UnityEngine;
namespace LonecraftGames.Toolkit.Core.Events
{
    [CreateAssetMenu(fileName = "SoundEventChannel", menuName = "LonecraftGames/Events/Audio/OneShotSoundEventChannel")]
    public class OneShotSoundEventChannel : EventChannel<SoundEventData> { }
}
using LonecraftGames.Toolkit.Core.Data;
using UnityEngine;

namespace LonecraftGames.Toolkit.Core.Events
{
    [CreateAssetMenu(fileName = "SoundEventChannel", menuName = "LonecraftGames/Events/Audio/SoundEventChannel")]
    public class SoundEventChannel : EventChannel<SoundEventData>
    {
        // This class can be used to define specific behaviors or properties for sound events.
        // Currently, it inherits all functionality from EventChannel<SoundEventData>.
    }
}
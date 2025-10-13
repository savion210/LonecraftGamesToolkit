using UnityEngine;

namespace LonecraftGames.Toolkit.Audio
{
    [CreateAssetMenu(fileName = "AudioSettings", menuName = "LonecraftGames/Audio/AudioSettings")]
    public class AudioSettings : ScriptableObject
    {
        public float masterVolume = 1f;
        public float musicVolume = 1f;
        public float ambientVolume = 1f;
        public float effectsVolume = 1f;
    }
}
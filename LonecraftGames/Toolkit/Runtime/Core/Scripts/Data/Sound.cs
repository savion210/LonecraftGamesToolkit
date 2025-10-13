using LonecraftGames.Toolkit.Core.Utilis;
using UnityEngine;

namespace LonecraftGames.Toolkit.Core.Data
{
    [System.Serializable]
    public class Sound
    {
        public Enums.AudiopClipEnum name;
        public AudioClip clip;
        public Enums.SoundCategory category;
        public bool loop;
        public bool spatial;
        [HideInInspector]
        public Vector3 position;
        [Range(0f, 1f)] public float volume = 1f;
    }
}
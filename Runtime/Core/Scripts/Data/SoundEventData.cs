using System;
using LonecraftGames.Toolkit.Core.Utilis;
using UnityEngine;

namespace LonecraftGames.Toolkit.Core.Data
{
    [Serializable]
    public class SoundEventData
    {
        public Enums.AudiopClipEnum soundName;
        public Vector3 position;

        public SoundEventData(Enums.AudiopClipEnum soundName, Vector3 position)
        {
            this.soundName = soundName;
            this.position = position;
        }
    }
}
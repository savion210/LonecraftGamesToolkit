using LonecraftGames.Toolkit.Core;
using LonecraftGames.Toolkit.Core.Data;
using LonecraftGames.Toolkit.Core.Events;
using LonecraftGames.Toolkit.Core.Utilis;
using UnityEngine;

namespace LonecraftGames.Toolkit.Audio
{
    [AddComponentMenu("LonecraftGames/Audio/SpacialTriggerSound")]
    public class SpacialTriggerSound : MonoBehaviour
    {
        [Header("Event Channels")]
        [SerializeField]
        private SoundEventChannel soundEventChannel;

        [Header("Sound Settings")]
        [SerializeField]
        private Enums.AudiopClipEnum soundName = Enums.AudiopClipEnum.ButtonClick;


        private void PlaySound()
        {
            SoundEventData soundEventData = new SoundEventData(soundName, transform.position);
            soundEventChannel.Raise(soundEventData);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) {
                PlaySound();
            }
        }
    }
}

using LonecraftGames.Toolkit.Core;
using LonecraftGames.Toolkit.Core.Data;
using LonecraftGames.Toolkit.Core.Events;
using LonecraftGames.Toolkit.Core.Utilis;
using UnityEngine;
using UnityEngine.UI;

namespace LonecraftGames.Toolkit.Audio
{
    [DisallowMultipleComponent]
    [AddComponentMenu("LonecraftGames/Audio/ButtonSound")]
    public class ButtonSound : MonoBehaviour
    {
        [Header("Event Channels")] [SerializeField]
        private SoundEventChannel soundEventChannel;

        [Header("Sound Settings")] [SerializeField]
        private Enums.AudiopClipEnum soundName = Enums.AudiopClipEnum.ButtonClick;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(PlaySound);
        }

        private void PlaySound()
        {
            SoundEventData soundEventData = new SoundEventData(soundName, transform.position);
            soundEventChannel.Raise(soundEventData);
        }
    }
}
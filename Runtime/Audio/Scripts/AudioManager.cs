using System.Collections.Generic;
using LonecraftGames.Toolkit.Core;
using LonecraftGames.Toolkit.Core.Data;
using LonecraftGames.Toolkit.Core.Events;
using LonecraftGames.Toolkit.Core.Utilis;
using UnityEngine;

namespace LonecraftGames.Toolkit.Audio
{
    [AddComponentMenu("LonecraftGames/Audio/AudioManager")]
    public class AudioManager : Singleton<AudioManager>
    {
        public AudioSettings AudioSettings => audioSettings;

        [Header("Settings")] [SerializeField] private AudioSettings audioSettings;

        [Header("Sounds")] [SerializeField] private List<Sound> sounds = new List<Sound>();

        [Header("Event Channels")] [SerializeField]
        private SoundEventChannel soundEventChannel;
        [SerializeField] private StopAudioEvent stopAudioEvent;

        private Dictionary<string, AudioSource> _soundSources = new Dictionary<string, AudioSource>();


        private void OnEnable()
        {
            soundEventChannel.RegisterListener(PlaySoundEvent);
            stopAudioEvent.RegisterListener(StopSoundEvent);
        }

        private void OnDisable()
        {
            soundEventChannel.UnregisterListener(PlaySoundEvent);
            stopAudioEvent.UnregisterListener(StopSoundEvent);
        }

        private void PlaySoundEvent(SoundEventData eventData)
        {
            Play(eventData.soundName.ToString(), eventData.position);
        }
        private void StopSoundEvent(string eventData)
        {
            Stop(eventData);
        }


        protected override void Awake()
        {
            DontDestroyOnLoad(gameObject);
            InitializeSounds();
        }

        private void Start()
        {
            //play background music
            SoundEventData soundEventData = new SoundEventData(Enums.AudiopClipEnum.BackgroundMusic1, Vector3.zero);
            soundEventChannel.Raise(soundEventData);
        }

        private void InitializeSounds()
        {
            foreach (var sound in sounds)
            {
                GameObject soundObject = new GameObject($"Sound_{sound.name}");
                soundObject.transform.parent = transform;
                AudioSource audioSource = soundObject.AddComponent<AudioSource>();
                audioSource.clip = sound.clip;
                audioSource.loop = sound.loop;
                audioSource.spatialBlend = sound.spatial ? 1f : 0f;
                audioSource.volume = sound.volume * GetCategoryVolume(sound.category);
                _soundSources[sound.name.ToString()] = audioSource;
            }
        }

        private void Play(string soundName, Vector3 position = default)
        {
            if (_soundSources.TryGetValue(soundName, out var source))
            {
                if (source.spatialBlend > 0 && position != default)
                {
                    source.transform.position = position;
                }

                source.Play();
            }
            else
            {
                Debug.LogWarning($"Sound {soundName} not found!");
            }
        }

        private float GetCategoryVolume(Enums.SoundCategory category)
        {
            float volume = 1f;
            switch (category)
            {
                case Enums.SoundCategory.Master:
                    volume = audioSettings.masterVolume;
                    break;
                case Enums.SoundCategory.Music:
                    volume = audioSettings.musicVolume;
                    break;
                case Enums.SoundCategory.Ambient:
                    volume = audioSettings.ambientVolume;
                    break;
                case Enums.SoundCategory.Effects:
                    volume = audioSettings.effectsVolume;
                    break;
            }

            return volume * audioSettings.masterVolume;
        }


        public void Stop(string soundName)
        {
            if (_soundSources.ContainsKey(soundName))
            {
                _soundSources[soundName].Stop();
            }
        }

        public void UpdateVolume(Enums.SoundCategory category, float volume)
        {
            switch (category)
            {
                case Enums.SoundCategory.Master:
                    audioSettings.masterVolume = volume;
                    break;
                case Enums.SoundCategory.Music:
                    audioSettings.musicVolume = volume;
                    break;
                case Enums.SoundCategory.Ambient:
                    audioSettings.ambientVolume = volume;
                    break;
                case Enums.SoundCategory.Effects:
                    audioSettings.effectsVolume = volume;
                    break;
            }

            ApplyVolumeSettings();
        }

        private void ApplyVolumeSettings()
        {
            foreach (var sound in sounds)
            {
                if (_soundSources.TryGetValue(sound.name.ToString(), out var source))
                {
                    source.volume = sound.volume * GetCategoryVolume(sound.category);
                }
            }
        }
    }
}
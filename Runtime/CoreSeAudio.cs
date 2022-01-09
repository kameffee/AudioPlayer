using UnityEngine;

namespace Kameffee.AudioPlayer
{
    [AddComponentMenu("")]
    public class CoreSeAudio : MonoBehaviour
    {
        public AudioSource AudioSource
        {
            get
            {
                if (_audioSource == null)
                {
                    if (!TryGetComponent<AudioSource>(out _audioSource))
                    {
                        _audioSource = gameObject.AddComponent<AudioSource>();
                        _audioSource.playOnAwake = false;
                    }
                }

                return _audioSource;
            }
        }

        private AudioSource _audioSource;

        public bool IsPlaying => AudioSource.isPlaying;

        public float Pitch
        {
            get => AudioSource.pitch;
            set => AudioSource.pitch = value;
        }

        public void Play(AudioClip audioClip)
        {
            AudioSource.clip = audioClip;
            AudioSource.Play();
        }

        public void Stop()
        {
            AudioSource.Stop();
        }

        public void SetVolume(float volume)
        {
            AudioSource.volume = volume;
        }
    }
}
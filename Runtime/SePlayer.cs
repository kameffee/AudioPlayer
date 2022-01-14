using System;
using UnityEngine;

namespace Kameffee.AudioPlayer
{
    [AddComponentMenu("Audio/Se Player")]
    public sealed class SePlayer : MonoBehaviour
    {
        [SerializeField]
        private PlayType _playType;

        [SerializeField]
        private AudioClip _clip;

        public AudioClip Clip
        {
            get => _clip;
            set => _clip = value;
        }

        [SerializeField]
        private string _key;

        public string Key
        {
            get => _key;
            set => _key = value;
        }

        [SerializeField]
        [Range(-3, 3f)]
        private float _pitch = 1f;

        public float Pitch
        {
            get => _pitch;
            set => _pitch = value;
        }

        public void Play()
        {
            switch (_playType)
            {
                case PlayType.AudioClip:
                    Play(_clip);
                    break;
                case PlayType.Id:
                    Play(_key);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Play(string key)
        {
            Play(key, _pitch);
        }

        public void Play(string key, float pitch)
        {
            AudioPlayer.Instance.Se.Play(key, pitch);
        }

        public void Play(AudioClip audioClip)
        {
            AudioPlayer.Instance.Se.Play(audioClip, _pitch);
        }
    }
}

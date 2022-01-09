using System;
using UnityEngine;

namespace Kameffee.AudioPlayer
{
    public enum PlayType
    {
        AudioClip,
        Id,
    }

    [AddComponentMenu("Audio/Bgm Player")]
    public class BgmPlayer : MonoBehaviour
    {
        [SerializeField]
        private PlayType _playType;

        [SerializeField]
        private int _id;

        public int ID
        {
            get => _id;
            set => _id = value;
        }

        [SerializeField]
        private AudioClip _audioClip = null;

        public AudioClip AudioClip
        {
            get => _audioClip;
            set => _audioClip = value;
        }

        [SerializeField]
        [Range(-3, 3f)]
        private float _pitch = 1f;

        public float Pitch
        {
            get => _pitch;
            set => _pitch = value;
        }

        [Header("Fade Settings")]
        [SerializeField]
        private bool _ignoreTimeScale = false;

        [SerializeField]
        private float _fadeInTime = 0;

        [SerializeField]
        private float _fadeOutTime = 0;

        [SerializeField]
        private float _crossFadeTime = 3;

        public void Play()
        {
            switch (_playType)
            {
                case PlayType.AudioClip:
                    Play(_audioClip, _pitch);
                    break;
                case PlayType.Id:
                    Play(_id, _pitch);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Play(AudioClip audioClip)
        {
            Play(audioClip, _pitch);
        }

        public void Play(int id)
        {
            Play(id, _pitch);
        }

        public void Play(int id, float pitch)
        {
            AudioPlayer.Instance.Bgm.Play(id, pitch, _fadeInTime, _ignoreTimeScale);
        }

        public void Play(AudioClip audioClip, float pitch)
        {
            AudioPlayer.Instance.Bgm.Play(audioClip, pitch, _fadeInTime, _ignoreTimeScale);
        }

        public void CrossFade(int id)
        {
            AudioPlayer.Instance.Bgm.CrossFade(id, _crossFadeTime, _pitch, _ignoreTimeScale);
        }

        public void CrossFade(AudioClip audioClip)
        {
            AudioPlayer.Instance.Bgm.CrossFade(audioClip, _crossFadeTime, _pitch, _ignoreTimeScale);
        }

        public void Pause()
        {
            AudioPlayer.Instance.Bgm.Pause();
        }

        public void UnPause()
        {
            AudioPlayer.Instance.Bgm.UnPause();
        }

        public void Stop()
        {
            Stop(_fadeOutTime);
        }

        public void Stop(float fadeOutTime)
        {
            AudioPlayer.Instance.Bgm.Stop(fadeOutTime, _ignoreTimeScale);
        }
    }
}
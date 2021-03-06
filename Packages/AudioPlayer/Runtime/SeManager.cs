using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kameffee.AudioPlayer
{
    public sealed class SeManager : MonoBehaviour, ISePlayer
    {
        private static readonly string SeManagerName = "SeManager";
        private static readonly string SePlayerName = "SePlayer";

        private readonly List<CoreSeAudio> _sePlayers = new List<CoreSeAudio>();

        private int _preInstanceCount = 2;

        public float Volume => _volume;
        private float _volume;

        public event Action<float> OnChangeVolume = null;

        private ISeBundle _seBundle;

        public static SeManager Create()
        {
            var se = new GameObject(SeManagerName);
            DontDestroyOnLoad(se);
            return se.AddComponent<SeManager>();
        }

        public void Initialize(ISeBundle seBundle, float initialVolume = 1)
        {
            _seBundle = seBundle;
            _volume = initialVolume;

            for (int i = 0; i < _preInstanceCount; i++)
            {
                _sePlayers.Add(CreatePlayer());
            }
        }

        private CoreSeAudio CreatePlayer()
        {
            var obj = new GameObject(SePlayerName);
            obj.transform.SetParent(transform);
            var player = obj.AddComponent<CoreSeAudio>();
            player.SetVolume(_volume);
            return player;
        }

        public void Play(string key, float pitch = 1)
        {
            if (_seBundle == null)
            {
                Debug.LogError("SeBundle is not loaded.");
                return;
            }

            var seData = _seBundle.GetData(key);
            Play(seData.AudioClip, pitch);
        }

        public void Play(AudioClip audioClip, float pitch = 1f)
        {
            CoreSeAudio seAudio;
            if (!TryGetPlayablePlayer(out seAudio))
            {
                seAudio = CreatePlayer();
                _sePlayers.Add(seAudio);
            }

            seAudio.Pitch = pitch;
            seAudio.Play(audioClip);
        }

        private bool TryGetPlayablePlayer(out CoreSeAudio seAudio)
        {
            foreach (var player in _sePlayers)
            {
                if (!player.IsPlaying)
                {
                    seAudio = player;
                    return true;
                }
            }

            seAudio = null;
            return false;
        }

        /// <summary>
        /// 再生中のSEを全て停止する.
        /// </summary>
        public void Stop()
        {
            foreach (var seAudio in _sePlayers)
            {
                seAudio.Stop();
            }
        }

        public void SetVolume(float volume)
        {
            var toVolume = Mathf.Clamp01(volume);
            var isChange = _volume != toVolume;
            _volume = toVolume;

            foreach (var sePlayer in _sePlayers)
            {
                sePlayer.SetVolume(volume);
            }

            if (isChange)
            {
                OnChangeVolume?.Invoke(_volume);
            }
        }
    }
}

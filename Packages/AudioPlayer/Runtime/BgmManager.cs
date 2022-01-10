using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kameffee.AudioPlayer
{
    public sealed class BgmManager : MonoBehaviour, IBgmPlayer
    {
        private static readonly string ManagerName = "BgmManager";
        private static readonly string PlayerName = "BgmPlayer";

        private readonly List<CoreBgmAudio> _bgmPlayers = new List<CoreBgmAudio>();

        private int _preInstanceCount = 2;

        public bool IsPlaying => _bgmPlayers.Any(bgmAudio => bgmAudio.IsPlaying);

        public float Volume => _volume;
        private float _volume;

        public event Action<float> OnChangeVolume = null;

        private IBgmBundle _bgmBundle;

        private Coroutine _coroutine;

        public static BgmManager Create()
        {
            var bgm = new GameObject(ManagerName);
            DontDestroyOnLoad(bgm);
            return bgm.AddComponent<BgmManager>();
        }

        public void Initialize(IBgmBundle bgmBundle, float initialVolume = 1f)
        {
            _bgmBundle = bgmBundle;
            _volume = initialVolume;

            for (int i = 0; i < _preInstanceCount; i++)
            {
                _bgmPlayers.Add(CreatePlayer());
            }
        }

        private CoreBgmAudio CreatePlayer()
        {
            var obj = new GameObject(PlayerName);
            obj.transform.SetParent(transform);
            var player = obj.AddComponent<CoreBgmAudio>();
            player.SetVolume(_volume);
            return player;
        }

        public void Play(int id, float pitch = 1, float fadeTime = 0f, bool ignoreTimeScale = false)
        {
            var audioClip = _bgmBundle.GetData(id);
            Play(audioClip.AudioClip, pitch, fadeTime, ignoreTimeScale);
        }

        public void Play(AudioClip audioClip, float pitch = 1, float fadeTime = 0f, bool ignoreTimeScale = false)
        {
            if (audioClip == null)
            {
                throw new ArgumentException("AudioClip is null.");
            }

            var bgmPlayer = _bgmPlayers.FirstOrDefault(player => !player.IsPlaying);
            if (bgmPlayer == null)
            {
                bgmPlayer = _bgmPlayers[0];
            }

            bgmPlayer.Play(audioClip, pitch, fadeTime, ignoreTimeScale);
        }

        public void CrossFade(int id, float crossFadeTime, float pitch = 1, bool ignoreTimeScale = false)
        {
            var audioClip = _bgmBundle.GetData(id);
            if (audioClip == null)
            {
                Debug.LogError($"Not found AudioClip. id:{id}");
                return;
            }

            CrossFade(audioClip.AudioClip, crossFadeTime, pitch, ignoreTimeScale);
        }

        public void CrossFade(AudioClip audioClip, float crossFadeTime, float pitch = 1, bool ignoreTimeScale = false)
        {
            if (crossFadeTime < 0f)
            {
                Debug.LogWarning("crossFadeTime of less than 0 was specified.");
                crossFadeTime = 0f;
            }

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            // 再生してないBgmPlayer
            CoreBgmAudio inTarget = GetPlayableBgmPlayer();

            // 再生中のBgmPlayer
            CoreBgmAudio outTarget = _bgmPlayers.FirstOrDefault(player => player.IsPlaying);

            _coroutine =
                StartCoroutine(
                    CrossFadeCoroutine(audioClip, outTarget, inTarget, crossFadeTime, pitch, ignoreTimeScale));
        }

        private IEnumerator CrossFadeCoroutine(AudioClip audioClip,
            CoreBgmAudio outTarget, CoreBgmAudio inTarget,
            float fadeTime,
            float pitch = 1,
            bool ignoreFadeTime = false)
        {
            outTarget?.Stop(fadeTime, ignoreFadeTime);
            inTarget?.Play(audioClip, pitch, fadeTime, ignoreFadeTime);

            float time = 0;
            while (time < fadeTime)
            {
                time += Time.deltaTime;
                yield return null;
            }

            _coroutine = null;
        }

        private CoreBgmAudio GetPlayableBgmPlayer()
        {
            var bgmPlayer = _bgmPlayers.FirstOrDefault(player => !player.IsPlaying);
            if (bgmPlayer == null)
            {
                bgmPlayer = CreatePlayer();
                _bgmPlayers.Add(bgmPlayer);
            }

            return bgmPlayer;
        }

        public void Stop(float fadeTime = 0f, bool ignoreTimeScale = false)
        {
            foreach (var player in _bgmPlayers)
            {
                player.Stop(fadeTime, ignoreTimeScale);
            }
        }

        public void Pause()
        {
            foreach (var player in _bgmPlayers)
            {
                player.Pause();
            }
        }

        public void UnPause()
        {
            foreach (var player in _bgmPlayers)
            {
                if (player.IsPause)
                {
                    player.UnPause();
                }
            }
        }

        public void SetVolume(float volume)
        {
            var toVolume = Mathf.Clamp01(volume);
            var isChange = _volume != toVolume;
            _volume = toVolume;

            foreach (var bgmPlayer in _bgmPlayers)
            {
                bgmPlayer.SetVolume(volume);
            }

            if (isChange)
            {
                OnChangeVolume?.Invoke(_volume);
            }
        }
    }
}

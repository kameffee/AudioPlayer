using System;
using System.Collections;
using UnityEngine;

namespace Kameffee.AudioPlayer
{
    [AddComponentMenu("")]
    public sealed class CoreBgmAudio : MonoBehaviour
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

        public bool IsPlaying => AudioSource.isPlaying;

        public bool IsPause { get; private set; }

        private AudioSource _audioSource;

        private float _currentVolume;

        private Coroutine _coroutine;

        public void Play(AudioClip audioClip, float pitch = 1, float fadeTime = 0, bool ignoreTimeScale = false)
        {
            AudioSource.loop = true;
            AudioSource.clip = audioClip;
            AudioSource.pitch = pitch;

            IsPause = false;

            if (fadeTime > 0f)
            {
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                _coroutine = StartCoroutine(FadeInCoroutine(fadeTime, ignoreTimeScale));
                return;
            }

            AudioSource.Play();
        }

        private IEnumerator FadeInCoroutine(float fadeTime, bool ignoreTimeScale = false)
        {
            AudioSource.Play();

            float time = 0;
            while (time < fadeTime)
            {
                if (ignoreTimeScale)
                {
                    time += Time.unscaledDeltaTime;
                }
                else
                {
                    time += Time.deltaTime;
                }

                AudioSource.volume = _currentVolume * (time / fadeTime);
                yield return null;
            }

            AudioSource.volume = _currentVolume;
            _coroutine = null;
        }

        public void Stop(float fadeTime = 0f, bool ignoreTimeScale = false)
        {
            IsPause = false;

            if (!IsPlaying)
            {
                return;
            }

            if (fadeTime > 0f)
            {
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                _coroutine = StartCoroutine(FadeOutCoroutine(fadeTime, ignoreTimeScale));
                return;
            }

            AudioSource.Stop();
        }

        private IEnumerator FadeOutCoroutine(float fadeTime, bool ignoreTimeScale = false)
        {
            float time = 0;
            float currentVolume = AudioSource.volume;
            while (time < fadeTime)
            {
                if (ignoreTimeScale)
                {
                    time += Time.unscaledDeltaTime;
                }
                else
                {
                    time += Time.deltaTime;
                }

                AudioSource.volume = (currentVolume - (time / fadeTime));
                yield return null;
            }

            AudioSource.volume = 0;
            AudioSource.Stop();
            _coroutine = null;
        }

        public void Pause()
        {
            if (!IsPlaying)
            {
                return;
            }

            AudioSource.Pause();
            IsPause = true;
        }

        public void UnPause()
        {
            if (IsPause)
            {
                AudioSource.UnPause();
                IsPause = false;
            }
        }

        public void SetVolume(float volume)
        {
            _currentVolume = volume;

            if (_coroutine == null)
            {
                AudioSource.volume = volume;
            }
        }
    }
}

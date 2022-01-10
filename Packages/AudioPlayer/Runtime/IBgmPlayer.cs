using System;
using UnityEngine;

namespace Kameffee.AudioPlayer
{
    public interface IBgmPlayer
    {
        bool IsPlaying { get; }

        float Volume { get; }

        event Action<float> OnChangeVolume;

        void Play(int id, float pitch = 1, float fadeTime = 0f, bool ignoreTimeScale = false);

        void Play(AudioClip audioClip, float pitch = 1, float fadeTime = 0f, bool ignoreTimeScale = false);

        void CrossFade(int id, float crossFadeTime, float pitch = 1, bool ignoreTimeScale = false);

        void CrossFade(AudioClip audioClip, float crossFadeTime, float pitch = 1, bool ignoreTimeScale = false);

        void Stop(float fadeTime = 0f, bool ignoreTimeScale = false);

        void Pause();

        void UnPause();

        void SetVolume(float volume);
    }
}

using System;
using UnityEngine;

namespace Kameffee.AudioPlayer
{
    public interface ISePlayer
    {
        float Volume { get; }

        event Action<float> OnChangeVolume;

        void Play(AudioClip audioClip, float pitch = 1f);
        
        void Play(string key, float pitch = 1f);

        void Stop();

        void SetVolume(float volume);
    }
}

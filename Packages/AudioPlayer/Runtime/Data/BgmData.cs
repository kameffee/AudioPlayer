using System;
using UnityEngine;

namespace Kameffee.AudioPlayer
{
    [Serializable]
    public class BgmData
    {
        public AudioClip AudioClip => _audioClip;
        public float Pitch => _pitch;

        [SerializeField]
        private AudioClip _audioClip;

        [SerializeField]
        [Range(-3f, 3f)]
        private float _pitch = 1f;
    }
}

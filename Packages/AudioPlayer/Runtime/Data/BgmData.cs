using System;
using UnityEngine;

namespace Kameffee.AudioPlayer
{
    [Serializable]
    public class BgmData
    {
        public AudioClip AudioClip => _audioClip;

        [SerializeField]
        private AudioClip _audioClip;
    }
}

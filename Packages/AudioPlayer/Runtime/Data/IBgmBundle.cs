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
        private float _pitch = 1f;
    }

    public interface IBgmBundle
    {
        BgmData GetData(int index);

        BgmData GetData(string name);
    }
}
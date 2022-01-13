using System;
using UnityEngine;

namespace Kameffee.AudioPlayer
{
    [Serializable]
    public class SeData
    {
        public string Key => _key;
        public AudioClip AudioClip => _audioClip;
        public float Pitch => _pitch;

        [SerializeField]
        private string _key = "SeKey_0";
        
        [SerializeField]
        private AudioClip _audioClip;

        [SerializeField]
        [Range(-3f, 3f)]
        private float _pitch = 1f;
    }
}

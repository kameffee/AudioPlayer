using System;
using UnityEngine;

namespace Kameffee.AudioPlayer
{
    [Serializable]
    public class SeData
    {
        public string Key => _key;
        public AudioClip AudioClip => _audioClip;

        [SerializeField]
        private string _key = "SeKey_0";

        [SerializeField]
        private AudioClip _audioClip;
    }
}

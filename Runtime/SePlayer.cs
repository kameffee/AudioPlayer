using UnityEngine;

namespace Kameffee.AudioPlayer
{
    [AddComponentMenu("Audio/Se Player")]
    public class SePlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _clip;

        public AudioClip Clip
        {
            get => _clip;
            set => _clip = value;
        }

        [SerializeField]
        [Range(-3, 3f)]
        private float _pitch = 1f;

        public float Pitch
        {
            get => _pitch;
            set => _pitch = value;
        }

        public void Play()
        {
            AudioPlayer.Instance.Se.Play(_clip);
        }

        public void Play(AudioClip audioClip)
        {
            AudioPlayer.Instance.Se.Play(audioClip, _pitch);
        }
    }
}
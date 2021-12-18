using System;
using Kameffee.AudioPlayer;
using UnityEngine;

namespace Samples.Scripts
{
    public class AudioDemo : MonoBehaviour
    {
        private void Start()
        {
            AudioPlayer.Instance.Initialize();
        }

        public void Play(int id)
        {
            AudioPlayer.Instance.Bgm.Play(id);
        }

        public void CrossFade(int id)
        {
            AudioPlayer.Instance.Bgm.CrossFade(id, 3f);
        }

        public void Stop()
        {
            AudioPlayer.Instance.Bgm.Stop();
        }
    }
}
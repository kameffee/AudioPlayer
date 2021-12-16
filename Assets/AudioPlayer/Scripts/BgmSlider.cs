using UnityEngine;
using UnityEngine.UI;

namespace Kameffee.AudioPlayer
{
    [AddComponentMenu("UI/Audio/Bgm Slider")]
    public class BgmSlider : MonoBehaviour
    {
        [SerializeField]
        private Slider bgmSlider;

        private void OnEnable()
        {
            bgmSlider.value = AudioPlayer.Instance.Bgm.Volume;
            bgmSlider.onValueChanged.AddListener(SetVolume);
        }

        private void OnDisable()
        {
            bgmSlider.onValueChanged.RemoveListener(SetVolume);
        }

        private void SetVolume(float volume)
        {
            AudioPlayer.Instance.Bgm.SetVolume(volume);
        }
    }
}
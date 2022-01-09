using UnityEngine;
using UnityEngine.UI;

namespace Kameffee.AudioPlayer
{
    [AddComponentMenu("UI/Audio/BgmSlider")]
    public class SeSlider : MonoBehaviour
    {
        [SerializeField]
        private Slider seSlider;

        private void OnEnable()
        {
            seSlider.value = AudioPlayer.Instance.Se.Volume;
            seSlider.onValueChanged.AddListener(SetVolume);
        }

        private void OnDisable()
        {
            seSlider.onValueChanged.RemoveListener(SetVolume);
        }

        private void SetVolume(float volume)
        {
            AudioPlayer.Instance.Se.SetVolume(volume);
        }
    }
}
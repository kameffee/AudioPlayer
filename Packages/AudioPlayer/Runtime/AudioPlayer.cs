using System;
using UnityEngine;

namespace Kameffee.AudioPlayer
{
    public sealed class AudioPlayer : IAudioPlayer
    {
        public static IAudioPlayer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AudioPlayer();
                    _instance.Initialize();
                }

                return _instance;
            }
        }

        private static AudioPlayer _instance;

        public IBgmPlayer Bgm => _bgmManager;
        private BgmManager _bgmManager;

        public ISePlayer Se => _seManager;
        private SeManager _seManager;

        public float MasterVolume => _masterVolume;
        private float _masterVolume;

        public event Action<float> OnChangeMasterVolume = null;

        public bool Initialized => _initialized;
        private bool _initialized;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void PreInitialize()
        {
            _instance = null;
        }

        public void Initialize()
        {
            if (_initialized)
            {
                Debug.Log("AudioManager initialized.");
                return;
            }

            InitializeBgm(null);
            InitializeSe();

            _initialized = true;
        }

        public void InitializeBgm(Func<IBgmBundle> bgmDataLoader = null)
        {
            if (bgmDataLoader == null)
            {
                bgmDataLoader = () => Resources.Load<BgmBundle>("BgmData");
            }

            // Initialize BGM
            _bgmManager = BgmManager.Create();
            _bgmManager.Initialize(bgmDataLoader.Invoke());
        }

        public void InitializeSe()
        {
            // Initialize SE
            _seManager = SeManager.Create();
            _seManager.Initialize();
        }

        public void SetMasterVolume(float volume)
        {
            var toVolume = Mathf.Clamp01(volume);
            bool isChange = Math.Abs(toVolume - _masterVolume) > 0.000001f;
            _masterVolume = toVolume;

            if (isChange)
            {
                OnChangeMasterVolume?.Invoke(toVolume);
            }
        }
    }
}

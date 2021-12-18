using System;
using UnityEngine;

namespace Kameffee.AudioPlayer
{
    public class AudioPlayer
    {
        public static AudioPlayer Instance
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

        public BgmManager Bgm => _bgmManager;
        private BgmManager _bgmManager;

        public SeManager Se => _seManager;
        private SeManager _seManager;

        public float MasterVolume
        {
            get => _masterVolume;
            set
            {
                var toVolume = Mathf.Clamp01(value);
                bool isChange = Math.Abs(toVolume - _masterVolume) > 0.000001f;
                _masterVolume = toVolume;

                if (isChange)
                {
                    OnChangeMasterVolume?.Invoke(toVolume);
                }
            }
        }

        private float _masterVolume;

        public event Action<float> OnChangeMasterVolume = null;

        public bool Initialized => _initialized;
        private bool _initialized;

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
    }
}
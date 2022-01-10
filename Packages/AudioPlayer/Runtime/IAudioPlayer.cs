using System;

namespace Kameffee.AudioPlayer
{
    public interface IAudioPlayer
    {
        float MasterVolume { get; }

        bool Initialized { get; }

        event Action<float> OnChangeMasterVolume;

        IBgmPlayer Bgm { get; }

        ISePlayer Se { get; }

        void Initialize();

        void SetMasterVolume(float volume);
    }
}

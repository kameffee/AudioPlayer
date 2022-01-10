# AudioPlayer
![Unity 2019.4+](https://img.shields.io/badge/unity-unity%202019.4%2B-blue)
![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)

## Introduction

### Initialize

```c#
// One time Initialize
AudioPlayer.Instance.Initialize();
```

### Play Bgm

#### Simple play
```c#
AudioPlayer.Instance.Bgm.Play(audioClip);
```

#### Cross Fade
```c#
AudioPlayer.Instance.Bgm.CrossFade(audioClip, crossFadeTime: 3f);
```

### Play Sound effects
```c#
AudioPlayer.Instance.Se.Play(audioClip);
```

### Volume settings

#### Change the volumes
```c#
// Master volume
AudioPlayer.Instance.SetMasterVolume(0.5f);

// Bgm volume
AudioPlayer.Instance.Bgm.SetVolume(1f); // ActualVolume 0.5f

// Se volume
AudioPlayer.Instance.Se.SetVolume(0.5f); // ActualVolume 0.25f
```

The actual volume of the BGM will be `0.5 x 1 = 0.5f`.  
The actual volume of the SE will be `0.5 x 0.5 = 0.25f`.

## Installation

### Installing from a Git URL
1. Copy for `https://github.com/kameffee/AudioPlayer.git`
2. Open a PackageManager. `Window/PackageManager`
3. Click the add **[+]** button in the status bar.
4. Select Add package from git URL from the add menu.
5. Enter a valid Git URL in the text box and click Add.

see for [Installing from a Git URL](https://docs.unity3d.com/2019.4/Documentation/Manual/upm-ui-giturl.html)

# Between Immersion and Usability: A Comparative Study of 2D and Mixed Reality Interfaces for Remote

[![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/i5TAI3mJ09E/0.jpg)](https://www.youtube.com/watch?v=i5TAI3mJ09E)

# How to use the system

> If you want to use our data (video and audio recordings) please drop an email to alberto.boem@unitn.it

## Recording Mode

For recording the audio and video data:

```sh
av_data_rec.py
```

(Tested on Windows 10)

### Dependencies:

[Python (v3.8)](https://www.python.org/downloads/release/python-380/)

[Stereolabs ZED Python API (v4.0)](https://github.com/stereolabs/zed-python-api)

### For audio: 

[Cockos Reaper (v6.78)](https://www.reaper.fm/) 

You can check the inputs of your soundcard using:

```sh
soundcard_utils.py 
```

(Tested on Windows 10)

## Playing Mode

To play the recorded data in 2D and MR import the .svo and .wav files into the respective Unity project.

### Dependencies:

[ZED Plugin for Unity (v4.0.6)](https://github.com/stereolabs/zed-unity)

For MR only use the [SteamVR Plugin](https://assetstore.unity.com/packages/tools/integration/steamvr-plugin-32647) from the Unity Asset Store.

(Tested with Unity 2020.3.48f1 LTS) 

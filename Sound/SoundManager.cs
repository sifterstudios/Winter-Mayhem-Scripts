using System;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static class AudioConstants
    {
        public const string RaceProgress = "RaceProgress";
        public const string AirborneHeight = "IsAirborne";
        public const string HighCut = "HighCut";
        public const string PlayerVelocity = "PlayerVelocity";
        public const string MusicOn = "MusicON";
        public const string MusicOff = "MusicOFF";
        public const string AudioOn = "AudioON";
        public const string AudioOff = "AudioOFF";
        public const string MusicFocus = "MusicFocus";
    }

    // TODO: HighCut when UI menu is showing

    static SoundManager _instance;

    public static SoundManager Instance
    {
        get { return _instance; }
    }

    [SerializeField] Transform mainFmodListenerGameObject;

    readonly Dictionary<string, string> _soundIndex = new Dictionary<string, string>()
    {
        {"Collision_Wood", "{c6a7ad04-96ce-4c95-a086-d429bd422b0a}"},
        {"Collision_Stone", "{1a2f5ae6-4126-41df-9edd-152b7bc4adc4}"},
        {"Collision_Player", "{3e5f78c7-af66-4d2b-815d-56960c45f19f}"},
        {"Collision_Yeti", "{8e343424-01f8-421c-9c15-f97b6cd81478}"},
        {"Collision_Ground", "{4f209ec8-6dd1-468d-86f9-2a930aa28a5c}"},
        {"Collision_Ice", "{4f209ec8-6dd1-468d-86f9-2a930aa28a5c}"},
        {"Jump", "{94fd0eea-f19a-4ddf-a0fd-22859323779e}"},
        {"ChangeDirection", "{9aa57c6d-92d6-49d6-b3f7-3ef43231d10c}"},
        {"Kick", "{688bf9f8-d9f9-44be-97a7-117de3ea0dfb}"},
        {"Skiing", "{32be1557-69cd-4e61-bdc3-06a556159c4d}"},
        {"Taunt", "{21d11159-f17d-4c9e-bcf7-fe40679d70eb}"},
        {"Cheer", "{6db56848-83a2-4e37-9354-a3ccc5f64a7e}"},
        {"Scream", "{1585e6da-bfd9-40ab-95cf-27328f287567}"},
        {"Wind", "{995a4f88-7ab5-4fe2-ab79-b3ec37050b51}"},
        {"Walla", "{379f414f-1a95-4875-9713-5600e441d5ec}"},
        {"Forest_Ambience", "{26f89ade-6973-4d09-a593-bc37e908a103}"},
        {"MainMusic", "{8a66f720-5362-49c5-9a54-d68c21bfb660}"},
        {"MusicON", "{31415210-c9cb-4a10-9d07-f4c59265922d}"},
        {"MusicOFF", "{7bd230be-a2ef-482e-9bbf-d26c41a02589}"},
        {"AudioON", "{e5b71965-ba2c-481a-83e4-fffe726f082b}"},
        {"AudioOFF", "{b80baf60-3b1c-40de-a74c-6f68c89f66b6}"},
    };

    EventInstance _audioOnSnapshot;
    EventInstance _audioOffSnapshot;
    EventInstance _musicOnSnapshot;
    EventInstance _musicOffSnapshot;
    EventInstance _cheeringEvent;


    void Awake()
    {
        // Singleton Instance Check!
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        // Instantiation Snapshots
        _audioOnSnapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/AudioON");
        _audioOffSnapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/AudioOFF");
        _musicOnSnapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/MusicON");
        _musicOffSnapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/MusicOFF");

        // Event Instantiation
        _cheeringEvent = FMODUnity.RuntimeManager.CreateInstance("{6db56848-83a2-4e37-9354-a3ccc5f64a7e}");

        // Make sure the audio levels are correct on Start()
        AudioOn();
    }
    public void PlaySoundNoParameters(string soundName, Transform transform = null, bool enableDebug = false)
    {
        var soundGUID = _soundIndex[soundName];
        if (gameObject == null)
        {
            transform = mainFmodListenerGameObject;
        }
        if (!CheckThatSoundNameExists(soundName)) return;

        FMODUnity.RuntimeManager.PlayOneShot(soundGUID, transform.position);

        if (enableDebug)
        {
            print($"Just played {soundName} with GUID {soundGUID} at this location: {transform.position.ToString()}");
        }
    }

    bool CheckThatSoundNameExists(string soundName)
    {
        if (_soundIndex.ContainsKey(soundName) == false)
        {
            UnityEngine.Debug.LogWarning($"Missing sound {soundName}");
            return false;
        }

        return false;
    }

    public void PlayCollisionSound(string soundName, float velocity, Transform transform = null)
    {
        if (transform == null)
        {
            transform = mainFmodListenerGameObject.transform;
        }

        if (!CheckThatSoundNameExists(soundName)) return;


        var soundGuid = _soundIndex[soundName];
        if (gameObject == null)
        {
            transform = mainFmodListenerGameObject;
        }

        EventInstance collisionSoundInstance = FMODUnity.RuntimeManager.CreateInstance(soundGuid);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(AudioConstants.PlayerVelocity, velocity);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(collisionSoundInstance, transform);
        collisionSoundInstance.start();
    }

    public void SetParameter(string parameterName, float value)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(parameterName, value);
    }

    void OnDisable()
    {
        var playerBus = FMODUnity.RuntimeManager.GetBus("bus:/");
        playerBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void AudioOn()
    {
        _audioOnSnapshot.start();
        _audioOffSnapshot.stop(STOP_MODE.ALLOWFADEOUT);
    }

    public void AudioOff()
    {
        _audioOffSnapshot.start();
        _audioOnSnapshot.stop(STOP_MODE.ALLOWFADEOUT);
    }

    public void MusicOn()
    {
        _musicOnSnapshot.start();
        _musicOffSnapshot.stop(STOP_MODE.ALLOWFADEOUT);
    }

    public void MusicOff()
    {
        _musicOffSnapshot.start();
        _musicOnSnapshot.stop(STOP_MODE.ALLOWFADEOUT);
    }

    // public void StartCheering()
    // {
    //     FMODUnity.RuntimeManager.AttachInstanceToGameObject(CheeringEvent, mainFmodListenerGameObject);
    //     CheeringEvent.start();
    // }

    // public void StopCheering()
    // {
    //     CheeringEvent.stop(STOP_MODE.ALLOWFADEOUT);
    // }
}
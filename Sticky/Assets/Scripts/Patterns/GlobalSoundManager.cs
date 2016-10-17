/***********************************************************************************
 * GlobalSoundManager.cs
 * Generic sound manager from Generic Framework Project developed by Shaun Landy 
***********************************************************************************/

using UnityEngine;

public enum AudioType
{
    MUSIC = 0,
    EFFECT,
    COUNT
}

/// <summary>
/// Sound Manager class pools a list of possible sound objects that can be accessed from any script
/// </summary>
public class GlobalSoundManager : MonoBehaviour
{
    private static GlobalSoundManager soundManager = null;
    public static GlobalSoundManager instance { get { return soundManager; } }

    private IterativeBehaviourPool<AudioObject> audioGeneralEffectsPool = null;    //Canot be paused
    private IterativeBehaviourPool<AudioObject> audioGameplayEffectsPool = null;   //Can be paused
    [SerializeField] private AudioObject audioObjectPrefab = null;

    [SerializeField] private AudioObject gameplayMusic = null;
    [SerializeField] private AudioClip[] gameplayPlaylist = null;

    [SerializeField] private AudioObject pauseMusic = null;
    [SerializeField] private AudioClip[] pausePlaylist = null;

    private float masterVolume = 1.0f;
    private float musicVolume = 1.0f;
    private float effectsVolume = 1.0f;

    [SerializeField] private AudioClip[] sounds = null;

    private void Awake()
    {
        if (soundManager)
        {
            DestroyImmediate(this);
        }
        else
        {
            soundManager = this;
            audioGeneralEffectsPool = new IterativeBehaviourPool<AudioObject>(audioObjectPrefab, 2, transform);
            audioGameplayEffectsPool = new IterativeBehaviourPool<AudioObject>(audioObjectPrefab, 10, transform);
            gameplayMusic.PlayMusic(gameplayPlaylist);
        }
    }

    /// <summary>
    /// Pauses all gameplay sounds
    /// </summary>
    private void PauseGameplaySounds()
    {
        foreach (AudioObject _audio in audioGameplayEffectsPool.currentPool)
        {
            _audio.Pause();
        }
        pauseMusic.PlayMusic(pausePlaylist, 1.0f, 1.0f);
        gameplayMusic.Pause(true, 3.0f);
    }

    /// <summary>
    /// Resumes all gameplay sounds
    /// </summary>
    private void ResumeGameplaySounds()
    {
        foreach (AudioObject _audio in audioGameplayEffectsPool.currentPool)
        {
            _audio.Resume();
        }
        pauseMusic.FadeOut(FadeOutResult.STOP, 3.0f);
        gameplayMusic.Resume(true, 3.0f);
    }

    /// <summary>
    /// Master volume effects all sounds using the Audio Listener
    /// </summary>
    /// <param name="_volume"></param>
    public void ChangeVolumeMaster(float _volume)
    {
        masterVolume = _volume;
        AudioListener.volume = masterVolume;
    }

    /// <summary>
    /// Changes specifically the background music volumne
    /// </summary>
    /// <param name="_volume"></param>
    public void ChangeVolumeMusic(float _volume)
    {
        musicVolume = _volume;
    }

    /// <summary>
    /// Changes specifically the FX volume
    /// </summary>
    /// <param name="_volume"></param>
    public void ChangeVolumeEffects(float _volume)
    {
        effectsVolume = _volume;
        foreach (AudioObject _audio in audioGeneralEffectsPool.currentPool)
        {
            _audio.UpdateVolume(effectsVolume);
        }
        foreach (AudioObject _audio in audioGameplayEffectsPool.currentPool)
        {
            _audio.UpdateVolume(effectsVolume);
        }
    }

    /// <summary>
    /// Play a sound given a specific audio clip
    /// </summary>
    /// <param name="_clip">Audio clip to play</param>
    /// <param name="_pos">Where to play</param>
    /// <param name="_volume">Optional volume that will edited based on the volume parameters later</param>
    /// <param name="_pitch">Optional pitch will affect the speed of the clip</param>
    /// <param name="_loop">Optional bool that determines if the sound effect should loop</param>
    /// <param name="_generalSound">Optional bool that determines whether the sound can be paused</param>
    /// <returns>A pooled Audio Object</returns>
    public AudioObject PlaySoundEffect(AudioClip _clip, Vector3 _pos, float _volume = 1.0f, float _pitch = 1.0f, bool _loop = false, bool _generalSound = false)
    {
        AudioObject _audio = _generalSound ? audioGeneralEffectsPool.GetPooledObject() : audioGameplayEffectsPool.GetPooledObject();
        _audio.transform.position = _pos;
        _audio.PlayEffect(_clip, _volume, _pitch, _loop);
        return _audio;
    }

    /// <summary>
    /// Play a sound given a specific index
    /// </summary>
    /// <param name="_id">id of the audio clip to play</param>
    /// <param name="_pos">Where to play</param>
    /// <param name="_volume">Optional volume that will edited based on the volume parameters later</param>
    /// <param name="_pitch">Optional pitch will affect the speed of the clip</param>
    /// <param name="_loop">Optional bool that determines if the sound effect should loop</param>
    /// <param name="_generalSound">Optional bool that determines whether the sound can be paused</param>
    /// <returns>A pooled Audio Object</returns>
    public AudioObject PlaySoundEffect(int _id, Vector3 _pos, float _volume = 1.0f, float _pitch = 1.0f, bool _loop = false, bool _generalSound = false)
    {
        AudioObject _audio = _generalSound ? audioGeneralEffectsPool.GetPooledObject() : audioGameplayEffectsPool.GetPooledObject();
        _audio.transform.position = _pos;
        _audio.PlayEffect(sounds[_id], _volume, _pitch, _loop);
        return _audio;
    }

    /// <summary>
    /// Used by Audio Objects to adjust volume
    /// </summary>
    /// <param name="_type"></param>
    /// <returns></returns>
    public float GetVolume(AudioType _type)
    {
        switch(_type)
        {
            case AudioType.MUSIC:
                return musicVolume;
            case AudioType.EFFECT:
                return effectsVolume;
            default:
                return 0.0f;
        }
    }
}

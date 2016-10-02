using UnityEngine;

public enum AudioType
{
    MUSIC = 0,
    EFFECT,
    COUNT
}

public class GlobalSoundManager : MonoBehaviour
{
    private static GlobalSoundManager soundManager = null;
    public static GlobalSoundManager instance { get { return soundManager; } }

    private IterativeBehaviourPool<AudioObject> audioGeneralEffectsPool = null;
    private IterativeBehaviourPool<AudioObject> audioGameplayEffectsPool = null;
    [SerializeField] private AudioObject audioObjectPrefab = null;

    [SerializeField] private AudioObject gameplayMusic = null;
    [SerializeField] private AudioClip[] gameplayPlaylist = null;

    [SerializeField] private AudioObject pauseMusic = null;
    [SerializeField] private AudioClip[] pausePlaylist = null;

    private float masterVolume = 1.0f;
    private float musicVolume = 1.0f;
    private float effectsVolume = 1.0f;

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

    private void PauseGameplaySounds()
    {
        foreach (AudioObject _audio in audioGameplayEffectsPool.currentPool)
        {
            _audio.Pause();
        }
        pauseMusic.PlayMusic(pausePlaylist, 1.0f, 1.0f);
        gameplayMusic.Pause(true, 3.0f);
    }

    private void ResumeGameplaySounds()
    {
        foreach (AudioObject _audio in audioGameplayEffectsPool.currentPool)
        {
            _audio.Resume();
        }
        pauseMusic.FadeOut(FadeOutResult.STOP, 3.0f);
        gameplayMusic.Resume(true, 3.0f);
    }

    public void ChangeVolumeMaster(float _volume)
    {
        masterVolume = _volume;
        AudioListener.volume = masterVolume;
    }

    public void ChangeVolumeMusic(float _volume)
    {
        musicVolume = _volume;
    }

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

    public AudioObject PlaySoundEffect(AudioClip _clip, Vector3 _pos, float _volume = 1.0f, float _pitch = 1.0f, bool _loop = false, bool _generalSound = false)
    {
        AudioObject _audio = _generalSound ? audioGeneralEffectsPool.GetPooledObject() : audioGameplayEffectsPool.GetPooledObject();
        _audio.transform.position = _pos;
        _audio.PlayEffect(_clip, _volume, _pitch, _loop);
        return _audio;
    }

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

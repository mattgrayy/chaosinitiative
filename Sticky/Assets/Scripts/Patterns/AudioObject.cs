/***********************************************************************************
 * AudioObject.cs
 * Generic audio object from Generic Framework Project developed by Shaun Landy 
***********************************************************************************/

using UnityEngine;

public enum FadeOutResult
{
    STOP = 0,
    PAUSE,
    PLAYLIST_NEXT,
    COUNT
}

/// <summary>
/// A custom audio object script that holds the functionality to cooperate with the sound manager
/// </summary>
public class AudioObject : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource = null;

    private AudioType audioType = AudioType.COUNT;

    private float defaultVolume = 1.0f;
    private bool isPaused = false;

    private AudioClip[] playlist = null;
    private int playlistIndex = 0;

    private bool isFading = false;
    private float fadeDuration = 0.5f;
    private float fadeTime = 0.0f;
    private bool fadeIn = true;
    private FadeOutResult fadeOutResult = FadeOutResult.STOP;

    public bool isPlaying { get { return audioSource.isPlaying; } }

    /// <summary>
    /// Called from the sound manager to play sound effect
    /// </summary>
    /// <param name="_clip">Audio clip to play</param>
    /// <param name="_volume">Volume of the clip</param>
    /// <param name="_pitch">Speed of the clip</param>
    /// <param name="_loop">Whether the audio should loop</param>
    public void PlayEffect(AudioClip _clip, float _volume = 1.0f, float _pitch = 1.0f, bool _loop = false)
    {
        gameObject.SetActive(true);
        audioType = AudioType.EFFECT;
        isPaused = false;
        defaultVolume = _volume;
        playlist = null;
        audioSource.PlayOneShot(_clip);
        audioSource.pitch = _pitch;
        audioSource.loop = _loop;
        UpdateVolume(GlobalSoundManager.instance.GetVolume(audioType));
    }

    /// <summary>
    /// Called from the sound manager to play music
    /// </summary>
    /// <param name="_playlist">A playlist of music to switch between</param>
    /// <param name="_volume">Volume of the music</param>
    /// <param name="_pitch">Speed of the music</param>
    public void PlayMusic(AudioClip[] _playlist, float _volume = 1.0f, float _pitch = 1.0f)
    {
        gameObject.SetActive(true);
        audioType = AudioType.MUSIC;
        isPaused = false;
        defaultVolume = _volume;
        playlistIndex = 0;
        playlist = _playlist;
        audioSource.pitch = _pitch;
        audioSource.loop = false;
        isFading = false;
        if (audioSource.isPlaying)
        {
            FadeOut(FadeOutResult.PLAYLIST_NEXT);
        }
        else
        {
            audioSource.PlayOneShot(playlist[playlistIndex]);
            UpdateVolume(GlobalSoundManager.instance.GetVolume(audioType));
        }
    }

    private void Update()
    {
        //If finished playing audio
        if(!isPaused && !audioSource.isPlaying)
        {
            //Not a playlist then disable audio object
            if (playlist == null)
            {
                isFading = false;
                gameObject.SetActive(false);
            }
            //Move onto next audio clip in the playlist
            else
            {
                playlistIndex = playlistIndex == playlist.Length - 1 ? 0 : playlistIndex + 1;
                audioSource.PlayOneShot(playlist[playlistIndex]);
            } 
        }
        //If fading and playing then fade in or out over time
        else if(isFading)
        {
            if (audioSource.isPlaying)
            {
                if (fadeIn)
                {
                    fadeTime += Time.deltaTime;
                    if (fadeTime >= fadeDuration)
                    {
                        UpdateVolume(GlobalSoundManager.instance.GetVolume(audioType));
                        isFading = false;
                    }
                    else
                    {
                        UpdateVolume((fadeTime / fadeDuration) * GlobalSoundManager.instance.GetVolume(audioType));
                    }
                }
                else
                {
                    fadeTime -= Time.deltaTime;
                    if (fadeTime <= 0)
                    {
                        switch (fadeOutResult)
                        {
                            case FadeOutResult.STOP:
                                Stop();
                                break;
                            case FadeOutResult.PAUSE:
                                audioSource.Pause();
                                break;
                            case FadeOutResult.PLAYLIST_NEXT:
                                audioSource.PlayOneShot(playlist[playlistIndex]);
                                UpdateVolume(GlobalSoundManager.instance.GetVolume(audioType));
                                break;
                        }
                        isFading = false;
                    }
                    else
                    {
                        UpdateVolume((fadeTime / fadeDuration) * GlobalSoundManager.instance.GetVolume(audioType));
                    }
                }
            }
        }
    }

    /// <summary>
    /// Updates volume from the sound manager
    /// </summary>
    /// <param name="_volMod"></param>
    public void UpdateVolume(float _volMod)
    {
        audioSource.volume = defaultVolume * _volMod;
    }

    /// <summary>
    /// Called from sound manager to pause audio
    /// </summary>
    /// <param name="_fadeOut">Whether the music should fade out</param>
    /// <param name="_fadeDuration">The duration to fade out</param>
    public void Pause(bool _fadeOut = false, float _fadeDuration = 0.5f)
    {
        isFading = false;
        if (_fadeOut)
        {
            FadeOut(FadeOutResult.PAUSE, _fadeDuration);
        }
        else
        {
            audioSource.Pause();
        }
        isPaused = true;
    }

    /// <summary>
    /// Called from sound manager to resume audio
    /// </summary>
    /// <param name="_fadeIn">Whether the music should fade in</param>
    /// <param name="_fadeDuration">The duration to fade in</param>
    public void Resume(bool _fadeIn = false, float _fadeDuration = 0.5f)
    {
        audioSource.UnPause();
        isPaused = false;
        isFading = false;
        if (_fadeIn)
        {
            FadeIn(_fadeDuration);
        }
    }

    /// <summary>
    /// Stops and turns off an audio object
    /// </summary>
    public void Stop()
    {
        audioSource.Stop();
        isFading = false;
        gameObject.SetActive(false);
    }

    public void FadeIn(float _duration = 0.5f)
    {
        audioSource.volume = 0.0f;
        fadeDuration = _duration;
        fadeTime = 0.0f;
        fadeIn = true;
        isFading = true;
    }

    public void FadeOut(FadeOutResult _result = FadeOutResult.STOP, float _duration = 0.5f)
    {
        fadeOutResult = _result;
        fadeDuration = _duration;
        fadeTime = fadeDuration;
        fadeIn = false;
        isFading = true;
    }
}

/***********************************************************************************
 * ParticleEffect.cs
 * Generic particle effect from Generic Framework Project developed by Shaun Landy 
***********************************************************************************/

using UnityEngine;

/// <summary>
/// A custom particle effect script that holds the functionality to cooperate with the particle manager
/// </summary>
public class ParticleEffect : MonoBehaviour
{
    [SerializeField] private string particleEffectName = string.Empty;  //A unique name for the particle effect
    public string particleName { get { return particleEffectName; } }

    [SerializeField] protected ParticleSystem[] particles = null;  //A list of all particle systems that are to be controlled by this script

    private bool isPaused = false;

    [SerializeField] protected float lifetime = 1.0f;  //Ensures the particle effect is returned to the pool 
    protected float currentLifetime = 0.0f;

    /// <summary>
    /// Plays the particle effect
    /// </summary>
    public virtual void Trigger()
    {
        gameObject.SetActive(true);
        isPaused = false;
        foreach (ParticleSystem p in particles)
        {
            p.Play();
        }
    }

    /// <summary>
    /// Ensures the object returns to the pool after a specific time
    /// </summary>
    protected virtual void Update()
    {
        if (!isPaused)
        {
            currentLifetime += Time.deltaTime;
            if (currentLifetime == lifetime)
            {
                Reset();
            }
        }
    }

    /// <summary>
    /// Pauses all particle system, called from particle manager
    /// </summary>
    public void Pause()
    {
        isPaused = true;
        foreach (ParticleSystem p in particles)
        {
            p.Pause();
        }
    }

    /// <summary>
    /// Resumes all particle systems, called from particle manager
    /// </summary>
    public void Resume()
    {
        isPaused = false;
        foreach (ParticleSystem p in particles)
        {
            p.Play();
        }
    }

    /// <summary>
    /// Returns particle effect to the pool
    /// </summary>
    protected virtual void Reset()
    {
        foreach (ParticleSystem p in particles)
        {
            p.Stop();
        }
        currentLifetime = 0.0f;
        gameObject.SetActive(false);
    }
}

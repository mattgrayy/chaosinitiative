using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    [SerializeField] private string particleEffectName = string.Empty;
    public string particleName { get { return particleEffectName; } }

    [SerializeField] protected ParticleSystem[] particles = null;

    private bool isPaused = false;

    [SerializeField] protected float lifetime = 1.0f;
    protected float currentLifetime = 0.0f;

    public virtual void Trigger()
    {
        gameObject.SetActive(true);
        isPaused = false;
        foreach (ParticleSystem p in particles)
        {
            p.Play();
        }
    }

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

    public void Pause()
    {
        isPaused = true;
        foreach (ParticleSystem p in particles)
        {
            p.Pause();
        }
    }

    public void Resume()
    {
        isPaused = false;
        foreach (ParticleSystem p in particles)
        {
            p.Play();
        }
    }

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

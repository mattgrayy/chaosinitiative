/***********************************************************************************
 * ParticleManager.cs
 * Generic particle manager from Generic Framework Project developed by Shaun Landy 
***********************************************************************************/

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Particle Manager class pools a list of possible particle effects that can be accessed from any script
/// </summary>
public class ParticleManager : MonoBehaviour
{
    private static ParticleManager singleton = null;
    public static ParticleManager instance { get { return singleton; } }

    [SerializeField] private ParticleEffect[] particleEffects = null;   //Array of all particle effects prefabs that will be used

    //Dictionary that holds all the particle effect pools accessable by integer index or string ID
    private Dictionary<string, IterativeBehaviourPool<ParticleEffect>> particleDictionary = new Dictionary<string, IterativeBehaviourPool<ParticleEffect>>();

    private void Awake()
    {
        if (singleton)
        {
            DestroyImmediate(this);
        }
        else
        {
            singleton = this;
            for(int i = 0; i < particleEffects.Length; ++i)
            {
                particleDictionary.Add(particleEffects[i].particleName, new IterativeBehaviourPool<ParticleEffect>(particleEffects[i], 5));
            }
        }
    }

    /// <summary>
    /// Get a particle using particle name
    /// </summary>
    /// <param name="_name"></param>
    /// <returns>An activated particle effect</returns>
    public ParticleEffect GetParticle(string _name)
    {
        IterativeBehaviourPool<ParticleEffect> pool;
        particleDictionary.TryGetValue(_name, out pool);
        if(pool != null)
        {
            return pool.GetPooledObject();
        }
        return null;
    }

    /// <summary>
    /// Get a particle using index
    /// </summary>
    /// <param name="_index"></param>
    /// <returns>An activated particle effect</returns>
    public ParticleEffect GetParticle(int _index)
    {
        if (_index > -1 && _index < particleDictionary.Count)
        {
            return particleDictionary.Values.ToList()[_index].GetPooledObject();
        }
        return null;
    }

    /// <summary>
    /// Calling will pause all currently active particle effects within this dictionary
    /// </summary>
    private void PauseGameplayParticles()
    {
        for(int i = 0; i < particleDictionary.Count; ++i)
        {
            foreach (ParticleEffect _particle in particleDictionary.Values.ToList()[i].currentPool)
            {
                _particle.Pause();
            }
        }
    }

    /// <summary>
    /// Calling will resume all currently paused particle effects within this dictionary
    /// </summary>
    private void ResumeGameplayParticles()
    {
        for (int i = 0; i < particleDictionary.Count; ++i)
        {
            foreach (ParticleEffect _particle in particleDictionary.Values.ToList()[i].currentPool)
            {
                _particle.Resume();
            }
        }
    }
}

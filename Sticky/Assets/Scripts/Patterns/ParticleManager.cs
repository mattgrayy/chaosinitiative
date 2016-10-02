using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ParticleManager : MonoBehaviour
{
    private static ParticleManager singleton = null;
    public static ParticleManager instance { get { return singleton; } }

    [SerializeField] private ParticleEffect[] particleEffects = null;

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

    public ParticleEffect GetParticle(int _index)
    {
        if (_index > -1 && _index < particleDictionary.Count)
        {
            return particleDictionary.Values.ToList()[_index].GetPooledObject();
        }
        return null;
    }

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

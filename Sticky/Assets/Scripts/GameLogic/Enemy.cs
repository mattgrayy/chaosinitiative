using UnityEngine;
using System.Collections;

public class Enemy : Actor
{
    [SerializeField] private ProjType[] projectileTypes = null;

    public Enemy GetProjectile()
    {
        //Randomly pick a projectile type from available types
        //Get pooled projectile of that type through projectile manager
        return ProjectileManager.instance.GetPooledProjectile(projectileTypes[Random.Range(0, projectileTypes.Length)]);
    }
}

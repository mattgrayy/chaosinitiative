using UnityEngine;
using System.Collections;

public class Enemy : Actor
{
    [SerializeField] private ProjType[] projectileTypes = null;

    public int interval = 1;
    public float intervalTimer = 0;

    public Enemy GetProjectile()
    {
        //Randomly pick a projectile type from available types
        //Get pooled projectile of that type through projectile manager
        return ProjectileManager.instance.GetPooledProjectile(projectileTypes[Random.Range(0, projectileTypes.Length)]);
    }

    void Update()
    {
        intervalTimer += Time.deltaTime;

        if (intervalTimer > interval)
        {
            GetProjectile();
            intervalTimer = 0;
        }
    }
}

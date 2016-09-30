using UnityEngine;
using System.Collections;

public class Enemy : Actor
{
    [SerializeField] private ProjType[] projectileTypes = null;

    [SerializeField] private float minFireRate = 3;
    [SerializeField] private float maxFireRate = 10;

    private float nextFireTime = 0;
    private float fireTimer = 0;

    private Transform myTransform;

    private void Awake()
    {
        myTransform = transform;

        nextFireTime = Random.Range(minFireRate, maxFireRate);
    }

    public void SetupEnemy(Vector3 startPosition)
    {
        myTransform.position = startPosition;
    }

    public BasicProjectile GetProjectile()
    {
        //Randomly pick a projectile type from available types
        //Get pooled projectile of that type through projectile manager
        return ProjectileManager.instance.GetPooledProjectile(projectileTypes[Random.Range(0, projectileTypes.Length)]);
    }

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= nextFireTime)
        {
            // Do rays
            BasicProjectile _proj = GetProjectile();
            _proj.FireProjectile(myTransform.position, myTransform.rotation);
            nextFireTime = Random.Range(minFireRate, maxFireRate);
            fireTimer = 0;
        }
    }
}

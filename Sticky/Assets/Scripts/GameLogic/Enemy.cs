using UnityEngine;
using System.Collections;

public class Enemy : Actor
{
    [SerializeField] private ProjType[] projectileTypes = null; //An array of the types of projectile that can be fired

    [SerializeField] private float minFireRate = 10;
    [SerializeField] private float maxFireRate = 30;
    [SerializeField] private Transform projectspawn;
    private bool isSetup = false;
    
    public static bool globalCanFire { get; set; }

    public static int numActiveEnemies { get; private set; } //total number of active enemies within the scene
    public static void ResetActiveEnemies() { numActiveEnemies = 0; } //function to reset numActiveEnemies if needed

    private float nextFireTime = 0;
    private float fireTimer = 0;
    public AudioSource Shoot;

    protected override void Awake()
    {
        base.Awake();
        nextFireTime = Random.Range(minFireRate, maxFireRate);
        fireTimer = Random.Range(0, nextFireTime * 0.5f);
    }

    public void SetupEnemy(Vector3 startPosition)
    {
        //Sanity check to prevent SetupEnemy() getting called multiple times
        if (!isSetup)
        {
            myTransform.position = startPosition;
            myTransform.rotation = Quaternion.identity;
            ++numActiveEnemies;
            isSetup = true;
        }
        
    }

    public BasicProjectile GetProjectile()
    {
        //Randomly pick a projectile type from available types
        //Get pooled projectile of that type through projectile manager
        return ProjectileManager.instance.GetPooledProjectile(projectileTypes[Random.Range(0, projectileTypes.Length)]);
    }

    void Update()
    {
        if (globalCanFire)
        {
            fireTimer += Time.deltaTime;
        }
        //Test Movement
        //transform.position += Vector3.down * Time.deltaTime * 0.25f;
        if (fireTimer >= nextFireTime)
        {
            //  rays
            Vector3 _dir = projectspawn.position - transform.position;

            RaycastHit2D hit = Physics2D.Raycast(projectspawn.position, _dir,1000);
            if (hit)
            {
                if (hit.collider.tag != "enemy")
                {
                    BasicProjectile _proj = GetProjectile();
                    _proj.FireProjectile(projectspawn.position, _dir);
                    //play shoot sound
                    Shoot.Play();
                    globalCanFire = false;
                }
            }
        
            nextFireTime = Random.Range(minFireRate, maxFireRate);
            fireTimer = 0;
        }
    }

    private void FixedUpdate()
    {
        Vector3 _dir = projectspawn.position - transform.position;
        ridg.AddForce(_dir * movementForce * Time.deltaTime);
        
    }

    public void Death()
    {
        //Sanity check to prevent Death() getting called multiple times
        if (isSetup)
        {
            gameObject.SetActive(false);
            --numActiveEnemies;
            isSetup = false;
            
        }
    }
    IEnumerator kill()
    {

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Death();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "enemy")
        {
            StartCoroutine("kill");
            ParticleEffect _particle = ParticleManager.instance.GetParticle(0);
            _particle.transform.position = myTransform.position;
            _particle.Trigger();
            
            
        }

    }

    
}

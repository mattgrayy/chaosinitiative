using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance { get; private set; }

    [SerializeField] private Transform parentFolder = null; //Sorting folder for pooled enemies
    [SerializeField] private Enemy[] enemyPrefabs = null; //Array of different enemy types as prefabs
    private IterativeBehaviourPool<Enemy>[] enemyPools = null; //Array of enemy pools for different enemy types

    [SerializeField] private float enemyFireDelay = 0.25f;
    private float fireDelayTime = 0.0f;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(this);
        }
        else
        {
            instance = this;
            Enemy.ResetActiveEnemies();
            Enemy.globalCanFire = false;
            //Create enemy pools from prefabs
            enemyPools = new IterativeBehaviourPool<Enemy>[enemyPrefabs.Length];
            for(int i = 0; i < enemyPrefabs.Length; ++i)
            {
                enemyPools[i] = new IterativeBehaviourPool<Enemy>(enemyPrefabs[i], 5, parentFolder);
            }
        }
    }

    private void Update()
    {
        if(!Enemy.globalCanFire)
        {
            fireDelayTime += Time.deltaTime;
            if(fireDelayTime >= enemyFireDelay)
            {
                fireDelayTime = 0.0f;
                Enemy.globalCanFire = true;
            }
        }
    }

    //Function to get a pooled enemy given an enemy id
    public Enemy GetPooledEnemy(int _ID)
    {
        return enemyPools[_ID].GetPooledObject();
    }
}

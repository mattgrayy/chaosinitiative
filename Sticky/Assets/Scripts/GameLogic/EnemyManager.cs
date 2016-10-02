using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance { get; private set; }

    [SerializeField] private Transform parentFolder = null; //Sorting folder for pooled enemies
    [SerializeField] private Enemy[] enemyPrefabs = null; //Array of different enemy types as prefabs
    private IterativeBehaviourPool<Enemy>[] enemyPools = null; //Array of enemy pools for different enemy types

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
            //Create enemy pools from prefabs
            enemyPools = new IterativeBehaviourPool<Enemy>[enemyPrefabs.Length];
            for(int i = 0; i < enemyPrefabs.Length; ++i)
            {
                enemyPools[i] = new IterativeBehaviourPool<Enemy>(enemyPrefabs[i], 5, parentFolder);
            }
        }
    }

    //Function to get a pooled enemy given an enemy id
    public Enemy GetPooledEnemy(int _ID)
    {
        return enemyPools[_ID].GetPooledObject();
    }
}

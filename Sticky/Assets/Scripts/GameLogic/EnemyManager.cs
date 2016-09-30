using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance { get; private set; }

    [SerializeField] private Transform parentFolder = null;
    [SerializeField] private Enemy[] enemyPrefabs = null;
    private IterativeBehaviourPool<Enemy>[] enemyPools = null;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(this);
        }
        else
        {
            instance = this;
            enemyPools = new IterativeBehaviourPool<Enemy>[enemyPrefabs.Length];
            for(int i = 0; i < enemyPrefabs.Length; ++i)
            {
                enemyPools[i] = new IterativeBehaviourPool<Enemy>(enemyPrefabs[i], 5, parentFolder);
            }
        }
    }

    public Enemy GetPooledEnemy(int _ID)
    {
        return enemyPools[_ID].GetPooledObject();
    }
}

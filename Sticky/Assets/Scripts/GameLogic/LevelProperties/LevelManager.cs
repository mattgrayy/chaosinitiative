using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }

    [SerializeField] private Level[] levels = null;
    [SerializeField] private Vector3 viewportEnemySpawn = Vector3.one;
    [SerializeField] private float nextLevelTime = 3.0f;
    private float levelTime = 0.0f;
    private int currentLevel = 0;
    private int currentWave = 0;
    private float waveTime = 0.0f;
    private float nextWaveTime = 0.0f;

    private bool hasLevelBegun = false;

    [SerializeField] private outcome outcme;

    public bool Winners = false;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(this);
        }
        else
        {
            instance = this;
        }
    }

    public void BeginLevel()
    {
        //Check there is a level available
        if (currentLevel != levels.Length)
        {
            hasLevelBegun = true;
        }
        //Finished all levels
        else
        {
            outcme.weWon();
            Winners = true;
            Application.LoadLevel(2);
        }
    }

    private void Update()
    {
        //Only spawn enemies if the level is in progress
        if (hasLevelBegun)
        {
            waveTime += Time.deltaTime;
            //Spawn the next wave
            if (waveTime >= nextWaveTime)
            {
                SpawnWave();
                ++currentWave;
                waveTime = 0.0f;
                //If finished all waves, prepare for next level
                if (currentWave == levels[currentLevel].waves.Length)
                {
                    ++currentLevel;
                    currentWave = 0;
                    nextWaveTime = 0.0f;
                    hasLevelBegun = false;
                }
                //If not finished prepare the timer for the next wave to spawn
                else
                {
                    nextWaveTime = levels[currentLevel].waves[currentWave].nextWaveTime;
                }
            }
        }
        else
        {
            //Only if all enemies have been defeated
            if (Enemy.numActiveEnemies == 0)
            {
                //Timer till the next level starts
                levelTime += Time.deltaTime;
                if (levelTime >= nextLevelTime)
                {
                    levelTime = 0.0f;
                    BeginLevel();
                }
            }
        }
    }

    private void SpawnWave()
    {
        //Get upper right position in viewport space to use as spawn position extents
        Vector3 _pos = Camera.main.ViewportToWorldPoint(viewportEnemySpawn);
        //The distance between each enemy
        float _x = (_pos.x * 2.0f) * 0.05f;
        //Spawn each enemy using an enemy id and location id
        foreach(WaveEnemyData waveEnemy in levels[currentLevel].waves[currentWave].enemyData)
        {
            EnemyManager.instance.GetPooledEnemy(waveEnemy.enemyID).SetupEnemy(new Vector3(_x * (waveEnemy.locationID - 10), _pos.y, 0.0f));
        }
    }
}
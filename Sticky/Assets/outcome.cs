using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class outcome : MonoBehaviour {

    public bool win = false;
    public int enemyKillCount = 0;
    public float gameTime = 0.0f;
    bool gameEnded = false;

    Text targetText;
    Text killText;
    Text timeText;

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Update()
    {
        if (!gameEnded)
        {
            gameTime += Time.deltaTime;
        }
    }

    public void getEnemyKillCount()
    {
        enemyKillCount = EnemyManager.instance.totalEnemiesKilled;
        gameEnded = true;
    }

    public void weWon()
    {
        win = true;
        getEnemyKillCount();
    }

    public void connectTargets(Text _tarText, Text _killText, Text _timeText)
    {
        targetText = _tarText;
        killText = _killText;
        timeText = _timeText;

        if (win)
        {
            targetText.text = "WINNERs";
            targetText.color = Color.green;
        }
        else
        {
            targetText.text = "GAME Over";
            targetText.color = Color.red;
        }

        killText.text = "Total Kills: " + enemyKillCount.ToString();
        timeText.text = "Time Played: " + ((int)gameTime).ToString();

        Destroy(gameObject);
    }
}

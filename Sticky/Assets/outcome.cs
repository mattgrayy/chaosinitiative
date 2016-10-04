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

    bool takenInfo = false;

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
        if (!takenInfo)
        {
            enemyKillCount = EnemyManager.instance.totalEnemiesKilled;
            gameEnded = true;
            takenInfo = true;
        }
    }

    public void weWon()
    {
        if (!takenInfo)
        {
            win = true;
            getEnemyKillCount();
            takenInfo = true;
        }
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
        timeText.text = "Time Played: " + ((int)gameTime).ToString() + " Seconds";

        Destroy(gameObject);
    }
}

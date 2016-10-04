using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverLinkage : MonoBehaviour {

    [SerializeField] private Text targetText;
    [SerializeField] private Text killText;
    [SerializeField] private Text timeText;

    [SerializeField] private int maxSceneTime;
    float sceneTimer = 0.0f;


    // Use this for initialization
    void Start ()
    {
        GameObject.FindWithTag("outcome").GetComponent<outcome>().connectTargets(targetText, killText, timeText);
	}

    void Update()
    {
        if (sceneTimer < maxSceneTime)
        {
            sceneTimer += Time.deltaTime;
        }
        else
        {
            Application.LoadLevel(0);
        }
    }
}

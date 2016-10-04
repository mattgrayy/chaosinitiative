using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class introTimer : MonoBehaviour {

    float timer = 0;
    [SerializeField] Text timerText;
    int time = 0;
    [SerializeField] int maxTime = 5;

    // Update is called once per frame
    void Update ()
    {
        timer += Time.deltaTime;
        if ((int)timer > time)
        {
            time = (int)timer;
            timerText.text = (maxTime - time).ToString();
            if (time == maxTime)
            {
                Destroy(gameObject);
            }
        }
	}
}

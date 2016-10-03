using UnityEngine;
using System.Collections;

public class MenuControl : MonoBehaviour {

    float baseScale = 0;
    bool bigger = false;

    void Start()
    {
        baseScale = transform.localScale.x;
    }
	
	// Update is called once per frame
	void Update () {

        if (bigger)
        {
            if (transform.localScale.x > baseScale + baseScale / 10)
            {
                bigger = false;
            }
            else
            {
                transform.localScale += new Vector3(0.01f, 0.01f, 0);
            }
        }
        else
        {
            if (transform.localScale.x < baseScale - baseScale / 10)
            {
                bigger = true;
            }
            else
            {
                transform.localScale -= new Vector3(0.1f, 0.1f, 0);
            }
        }

        if (Input.GetButtonDown("1Button1"))
        {
            //play
            Application.LoadLevel(0);
        }

        if (Input.GetButtonDown("2Button1"))
        {
            Application.Quit();
        }
    }
}

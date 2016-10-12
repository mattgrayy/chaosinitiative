using UnityEngine;
using System.Collections;

public class menu_control : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButtonDown("3Button1"))
        {
            Player.playerShare = !Player.playerShare;
        }

	if(Input.GetButtonDown("0Button1"))
        {
            Application.LoadLevel(1);
        }
        if (Input.GetButtonDown("0Button3"))
        {
            Application.Quit();
        }
        if (Input.GetButtonDown("1Button1"))
        {
            Application.LoadLevel(1);
        }
        if (Input.GetButtonDown("1Button3"))
        {
            Application.Quit();
        }
        if (Input.GetButtonDown( "2Button1"))
        {
            Application.LoadLevel(1);
        }
        if (Input.GetButtonDown("2Button3"))
        {
            Application.Quit();
        }
    }
}

using UnityEngine;
using System.Collections;

public class go : MonoBehaviour {

    public ParticleSystem ex1, ex2;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetKeyDown(KeyCode.C))
        {
            ex1.Play();
            ex2.Play();


        }



    }
}

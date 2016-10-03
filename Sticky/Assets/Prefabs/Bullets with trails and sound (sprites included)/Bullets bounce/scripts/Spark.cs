using UnityEngine;
using System.Collections;

public class Spark : MonoBehaviour {

    public Transform sparkle;
	//public AudioClip bounce;


	// Use this for initialization
	void Start () {
		sparkle.GetComponent<ParticleSystem> ().Stop();
		//GetComponent<AudioSource> ().clip = bounce;	
	}
	
	void OnTriggerEnter2D()
    {

		sparkle.GetComponent<ParticleSystem> ().Play();
        StartCoroutine(stopSparkles());
		//GetComponent<AudioSource> ().Play ();
    }

    IEnumerator stopSparkles()
    {
        yield return new WaitForSeconds(.1f);
		sparkle.GetComponent<ParticleSystem> ().Stop();

    }


}

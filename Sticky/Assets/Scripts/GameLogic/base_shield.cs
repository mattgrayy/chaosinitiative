using UnityEngine;
using System.Collections;

public class base_shield : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	
	}


    public void DestroySheild()
    {
        gameObject.SetActive(false);
    }

    void LateUpdate()
    {
    
    }

    IEnumerator kill()
    {
        
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        
        DestroySheild();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        

        StartCoroutine("kill");

       
      
    }


}



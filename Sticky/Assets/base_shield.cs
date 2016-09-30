using UnityEngine;
using System.Collections;

public class base_shield : MonoBehaviour {
    [SerializeField]  protected int ricocet = 1;
    private bool TIMETODIE = false;
	// Use this for initialization
	void Start () {
	
	}


    public void DestroySheild()
    {
        gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        if (ricocet < 1)
        {
           
        }
    }

    IEnumerator kill()
    {
        
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        
        DestroySheild();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        TIMETODIE = true;

        StartCoroutine("kill");

        if (ricocet>0)
        {
            ricocet--;
        }
      
    }


}



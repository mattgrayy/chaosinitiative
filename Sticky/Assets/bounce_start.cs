using UnityEngine;
using System.Collections;

public class bounce_start : MonoBehaviour {

    // Use this for initialization
    public float x;
    public float y;
    public Rigidbody2D ridg;
    public GameObject ball;
    public int bounces;
  
	void Start () {
        ridg = GetComponent<Rigidbody2D>();
        ridg.AddForce(new Vector2(x,y));
    }

    void OnCollision2D()
    {
    }


    
    void OnCollisionEnter2D(Collision2D col)
    {
       
        if (col.gameObject.tag == "wall3")
        {
         
            if (bounces > 0)
            {
                bounces--;
            }
            else if (bounces == 0)
            {
                Destroy(ball);
            }
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        
       
	}
}

using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{

    public Rigidbody2D bulletbody;

    // Use this for initialization
    void Start()
    {
        bulletbody = this.GetComponent<Rigidbody2D>();

        bulletbody.AddForce(new Vector2(0, -500));

    }

    // Update is called once per frame
    void Update()
    {
        



    }

}
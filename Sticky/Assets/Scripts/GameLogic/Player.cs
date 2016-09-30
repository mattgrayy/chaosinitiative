using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : Actor {

    public int playerNumber = 0;
    public SpriteRenderer shieldSprite;
    public ProjType shieldType;

    void Update()
    {
        switch (playerNumber)
        {
            case 1:
                transform.Translate(Vector2.right * Input.GetAxis("Mouse X") * 10 * Time.deltaTime); // Replace with "XAxis1"
                break;
            case 2:
                transform.Translate(Vector2.right * Input.GetAxis("Mouse Y") * 10 * Time.deltaTime); // Replace with "XAxis2"
                break;
            case 3:
                transform.Translate(Vector2.right * Input.GetAxis("Mouse ScrollWheel") * 100 * Time.deltaTime); // Replace with "XAxis3"
                break;
            default:
                break;
        }
        transform.Translate(Vector2.right * Input.GetAxis("XAxis" + (playerNumber + 1).ToString()) * 10 * Time.deltaTime);
    }

    /*
    void OnCollisionEnter(Collider col)
    {
        if (col.tag == "Projectile")
        {
            //if (col.GetComponent<>)
            //{

            //}
        }
    }*/
}

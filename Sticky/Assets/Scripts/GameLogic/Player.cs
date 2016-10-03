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
            case 0:
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * Input.GetAxis("XAxis" + (playerNumber + 1).ToString()) * movementForce * Time.deltaTime);
                //GetComponent<Rigidbody2D>().AddForce(Vector2.right * Input.GetAxis("Mouse X") * movementForce * 100 * Time.deltaTime);
                break;
            case 1:
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * Input.GetAxis("3Axis1") * movementForce * Time.deltaTime);
                break;
            case 2:
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * Input.GetAxis("7Axis1") * movementForce * Time.deltaTime);
                break;
            default:
                break;
        }

        //GetComponent<Rigidbody2D>().AddForce(Vector2.right * Input.GetAxis("XAxis" + (playerNumber + 1).ToString()) * movementForce * Time.deltaTime);

    }
}

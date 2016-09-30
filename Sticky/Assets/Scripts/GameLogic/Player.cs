using UnityEngine;
using System.Collections;

public class Player : Actor {

    public int playerNumber = 0;

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
    }
}

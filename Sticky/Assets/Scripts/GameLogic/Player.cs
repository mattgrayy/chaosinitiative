using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : Actor {

    public int playerNumber = 0;
    public SpriteRenderer shieldSprite;
    public ProjType shieldType;

    public Sprite BasicShield, DamageShield, KnockShield, VoidSheild;
    public string ShieldTag = "Basic";

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

        //check for button press
        if (Input.GetButtonDown("1Button" + (playerNumber + 1).ToString()))
        {
            shieldSprite.sprite = BasicShield;
            ShieldTag = "Basic";
        }

        if (Input.GetButtonDown("2Button" + (playerNumber + 1).ToString()))
        {
            shieldSprite.sprite = DamageShield;
            ShieldTag = "Damage";
        }

        if (Input.GetButtonDown("0Button" + (playerNumber + 1).ToString()))
        {
            shieldSprite.sprite = KnockShield;
            ShieldTag = "Knock";
        }

        if (Input.GetButtonDown("3Button" + (playerNumber + 1).ToString()))
        {
            shieldSprite.sprite = VoidSheild;
            ShieldTag = "Void";
        }

    }


    void OnCollisionEnter2D(Collision2D col)
    {

        if (!(col.gameObject.tag == ShieldTag))
        {

            //kill player
            gameObject.SetActive(false);

        }


    }





}

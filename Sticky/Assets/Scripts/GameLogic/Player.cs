using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : Actor {

    public int playerNumber = 0;
    public SpriteRenderer shieldSprite;
    public ProjType shieldType;
    public Sprite NormalShield, DeadlyBounceShield, KnockBounceShield, VoidShield;
    public string SheildTag = "Normal";

    //player shield type sprites
    //normal
    //bounce damage



    void Update()
    {

        //movment
        switch (playerNumber)
        {
            case 0:
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * Input.GetAxis("XAxis" + (playerNumber + 1).ToString()) * movementForce * Time.deltaTime);
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




       //changing the 
       //Normal shield (Cross)
       if (Input.GetButtonDown("1Button"+ (playerNumber + 1).ToString()))
        {
             shieldSprite.sprite = NormalShield;
            SheildTag = "Normal";

        }

        //DeadlyBounce shieled (square)
        if (Input.GetButtonDown("0Button" + (playerNumber + 1).ToString()))
        {
            shieldSprite.sprite = DeadlyBounceShield;
            SheildTag = "DeadlyBounce";
        }

        //(Circle)
        if (Input.GetButtonDown("2Button" + (playerNumber + 1).ToString()))
        {
            shieldSprite.sprite = KnockBounceShield;
            SheildTag = "KnockBounceShield";
        }


        //(Triangle)
        if (Input.GetButtonDown("3Button" + (playerNumber + 1).ToString()))
        {
            shieldSprite.sprite = VoidShield;
            SheildTag = "VoidShield";
        }

    }



    void OnCollisionEnter2D(Collision2D bullet)
    {
        //check if the tags are different
        if (!(bullet.gameObject.tag == SheildTag))
        {
            gameObject.SetActive(false);

        }


    }


}

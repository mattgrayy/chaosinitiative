using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : Actor {

    public int playerNumber = 0;
    public SpriteRenderer shieldSprite;
    public ProjType shieldType;

    public Sprite BasicShield, DamageShield, KnockShield, VoidSheild;
    public string ShieldTag = "Basic";

    [SerializeField] private SpriteRenderer[] spriteRenderers = null;
    private CircleCollider2D circleCollider = null;

    [SerializeField] private float respawnFlickerRate = 0.25f;
    private float respawnFlickerTime = 0.0f;
    [SerializeField] private float respawnRate = 2.0f;
    [SerializeField] private float respawnTime = 0.0f;
    private bool isRespawning = false;

    public static bool playerShare = false;

    [SerializeField] private ParticleSystem parSystem = null;
    protected override void Awake()
    {
        base.Awake();
        circleCollider = GetComponent<CircleCollider2D>();
        if(playerShare)
        {
            switch (playerNumber)
            {
                case 0:
                    shieldSprite.sprite = BasicShield;
                    ShieldTag = "Basic";
                    parSystem.Clear();
                    parSystem.startColor = Color.blue;
                    break;
                case 1:
                    shieldSprite.sprite = DamageShield;
                    ShieldTag = "Damage";
                    parSystem.Clear();
                    parSystem.startColor = Color.red;
                    break;
                case 2:
                    shieldSprite.sprite = KnockShield;
                    ShieldTag = "Knock";
                    parSystem.Clear();
                    parSystem.startColor = Color.magenta;
                    break;
                default:
                    break;
            }
        }
    }

    void Update()
    {
        if (isRespawning)
        {
            respawnTime += Time.deltaTime;
            if (respawnTime >= respawnRate)
            {
                isRespawning = false;
                circleCollider.enabled = true;
                parSystem.Play();
                foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                {
                    spriteRenderer.enabled = true;
                }
            }
            else
            {
                respawnFlickerTime += Time.deltaTime;
                if (respawnFlickerTime >= respawnFlickerRate)
                {
                    respawnFlickerTime = 0.0f;
                    foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                    {
                        spriteRenderer.enabled = !spriteRenderer.enabled;
                    }
                }
            }
        }
        else
        {
            if (playerShare)
            {
                switch (playerNumber)
                {
                    case 0:
                        ridg.AddForce(Vector2.right * Input.GetAxis("7Axis1") * movementForce * Time.deltaTime);
                        break;
                    case 1:
                        ridg.AddForce(Vector2.right * Input.GetAxis("XAxis1") * movementForce * Time.deltaTime);
                        break;
                    case 2:
                        ridg.AddForce(Vector2.right * Input.GetAxis("3Axis1") * movementForce * Time.deltaTime);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                ridg.AddForce(Vector2.right * Input.GetAxis("XAxis" + (playerNumber + 1).ToString()) * movementForce * Time.deltaTime);
            }
            //GetComponent<Rigidbody2D>().AddForce(Vector2.right * Input.GetAxis("XAxis" + (playerNumber + 1).ToString()) * movementForce * Time.deltaTime);
        }

        if (!playerShare)
        {
            //check for button press
            if (Input.GetButtonDown("0Button" + (playerNumber + 1).ToString()))
            {
                shieldSprite.sprite = BasicShield;
                ShieldTag = "Basic";
                parSystem.Clear();
                parSystem.startColor = Color.blue;
            }

            if (Input.GetButtonDown("1Button" + (playerNumber + 1).ToString()))
            {
                shieldSprite.sprite = DamageShield;
                ShieldTag = "Damage";
                parSystem.Clear();
                parSystem.startColor = Color.red;
            }

            if (Input.GetButtonDown("2Button" + (playerNumber + 1).ToString()))
            {
                shieldSprite.sprite = KnockShield;
                ShieldTag = "Knock";
                parSystem.Clear();
                parSystem.startColor = Color.magenta;
            }

            if (Input.GetButtonDown("3Button" + (playerNumber + 1).ToString()))
            {
                shieldSprite.sprite = VoidSheild;
                ShieldTag = "Void";
                parSystem.Clear();
                parSystem.startColor = Color.green;
            }
        }  
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {

        }
        else if (col.gameObject.tag == "wall")
        {

        }
        else if (!(col.gameObject.tag == ShieldTag))
        {
            if (!playerShare)
            {
                Death();
                BasicProjectile _proj = col.gameObject.GetComponent<BasicProjectile>();
                if (_proj)
                {
                    _proj.DestroyProjectile();
                }
            }
        }
    }

    private void Death()
    {
        //kill player
        ParticleEffect _particle = ParticleManager.instance.GetParticle(0);
        CameraShake2.instance.ShakeCamera(0.1f, 0.05f);
        _particle.transform.position = myTransform.position;
        _particle.Trigger();
        GlobalSoundManager.instance.PlaySoundEffect(Random.Range(0, 5), Vector3.zero, 0.25f);
        //gameObject.SetActive(false);
        isRespawning = true;
        respawnTime = 0.0f;
        respawnFlickerTime = 0.0f;
        parSystem.Clear();
        parSystem.Stop();
        circleCollider.enabled = false;
    }





}

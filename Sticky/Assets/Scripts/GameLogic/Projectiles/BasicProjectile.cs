using UnityEngine;
using System.Collections;

public class BasicProjectile : MonoBehaviour
{
    private bool lethal = true;
   
    private Transform myTransform = null;

    [SerializeField] protected bool canBounce = false;
    [SerializeField] protected int bounces = 6;
    [SerializeField] protected float projectileSpeed = 5.0f;
    public float x;
    public float y;
    [SerializeField] protected Rigidbody2D ridg;
    public GameObject ball;


    void Start()
    {
        ridg = GetComponent<Rigidbody2D>();
        
    }
    private void Awake()
    {
        myTransform = transform;
    }

    
    private void Update()
    {
      
       
   
    }

    private void LateUpdate()
    {
        ridg.velocity = Vector3.ClampMagnitude(ridg.velocity * 10, projectileSpeed);
    }

    public void FireProjectile(Vector3 _pos, Vector3 _dir)
    {
        myTransform.position = _pos;
        
        ridg.velocity = Vector2.zero;

        ridg.AddForce(new Vector2(_dir.x,_dir.y));
    }

    public void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }



    void OnCollisionEnter2D(Collision2D col)
    {

            if (bounces > 0)
            {
                bounces--;
            }
            else if (bounces == 0)
            {
            DestroyProjectile();
            }
        }
    

}



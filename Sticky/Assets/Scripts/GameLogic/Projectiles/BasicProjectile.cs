using UnityEngine;
using System.Collections;

public class BasicProjectile : MonoBehaviour
{
    private Transform myTransform = null;

    [SerializeField] protected int bounces = 2;
    protected int currentBounces = 2;
    [SerializeField] protected float projectileSpeed = 5.0f;
    public float x;
    public float y;
    [SerializeField] protected Rigidbody2D ridg;
    public GameObject ball;
    protected Enemy enemySpawnedFrom = null;


    void Start()
    {
        ridg = GetComponent<Rigidbody2D>();
        
    }
    private void Awake()
    {
        myTransform = transform;
    }

    private void LateUpdate()
    {
        ridg.velocity = Vector3.ClampMagnitude(ridg.velocity * 10, projectileSpeed);
    }

    public void FireProjectile(Vector3 _pos, Vector3 _dir, Enemy _enemy)
    {
        myTransform.position = _pos;
        enemySpawnedFrom = _enemy;
        ridg.velocity = Vector2.zero;
        currentBounces = bounces;
        ridg.AddForce(new Vector2(_dir.x,_dir.y));
        GetComponent<TrailRenderer>().Clear();
    }

    public void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }



   protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (currentBounces > 0)
        {
            if (col.gameObject.tag == "enemy")
            {
                currentBounces--;
            }
        }
        if (col.transform.tag == "Shield")
        {
            currentBounces = 0;
        }
        if (currentBounces == 0)
        {
            if (col.gameObject.tag == "enemy" || col.gameObject.tag == "Shield")
            {
                DestroyProjectile();
            }
        }
    }
}



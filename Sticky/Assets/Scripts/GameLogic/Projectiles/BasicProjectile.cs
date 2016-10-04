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
    private bool isDying = false;
  
    private void Awake()
    {
        myTransform = transform;
        ridg = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        ridg.velocity = Vector3.ClampMagnitude(ridg.velocity * 10, projectileSpeed);
    }

    public void FireProjectile(Vector3 _pos, Vector3 _dir)
    {
        isDying = false;
        myTransform.position = _pos;
        ridg.velocity = Vector2.zero;
        currentBounces = bounces;
        ridg.AddForce(new Vector2(_dir.x,_dir.y));
        GetComponent<TrailRenderer>().Clear();
    }

    public void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }

    IEnumerator kill()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        DestroyProjectile();
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (!isDying)
        {
            GlobalSoundManager.instance.PlaySoundEffect(Random.Range(5, 11), Vector3.zero, 0.15f);
            if (currentBounces > 0)
            {
                if (col.gameObject.tag == "enemy")
                {
                    if (tag != "Knock" || currentBounces == 1)
                    {
                        col.gameObject.GetComponent<Enemy>().HitByMurderousProjectile();
                    }
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
                    isDying = true;
                    StartCoroutine("kill");
                }
            }
        }
    }

}



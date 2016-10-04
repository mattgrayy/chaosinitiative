using UnityEngine;
using System.Collections;

public class VoidSuction : MonoBehaviour
{
    [SerializeField] private float suctionRadius = 5.0f;
    [SerializeField] private float suctionForce = 5.0f;

    [SerializeField] private float suctionRate = 2.0f;
    private float suctionTime = 0.0f;

    [SerializeField] private float minRotVelocity = 0.1f;
    [SerializeField] private float maxRotVelocity = 1.0f;

    public void StartSuction(Vector3 _pos)
    {
        transform.position = _pos;
        suctionTime = 0.0f;
        ParticleEffect par = ParticleManager.instance.GetParticle(1);
        par.transform.position = _pos;
        par.Trigger();
    }

    private void Update()
    {
        suctionTime += Time.deltaTime;
        if(suctionTime >= suctionRate)
        {
            StopSuction();
        }
    }

    private void FixedUpdate()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, suctionRadius, Vector2.zero);
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.rigidbody)
            {
                if (hit.rigidbody.tag == "enemy")
                {
                    hit.rigidbody.AddForce(Quaternion.AngleAxis(-35, Vector3.forward) * ((transform.position - hit.transform.position) * suctionForce));
                    hit.rigidbody.angularVelocity += Random.Range(minRotVelocity, maxRotVelocity) * Time.deltaTime;
                }
            }
        }
    }

    public void StopSuction()
    {
        gameObject.SetActive(false);
    }
}

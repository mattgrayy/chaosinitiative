using UnityEngine;
using System.Collections;

public class BasicProjectile : MonoBehaviour
{
    private ProjType type = ProjType.BASIC;
    private bool lethal = true;
    private int bounces = 6;

    private Transform myTransform = null;

    private void Awake()
    {
        myTransform = transform;
    }

    float temp = 0.0f;
    private void Update()
    {
        myTransform.position += (Vector3.down * Time.deltaTime);
        temp += Time.deltaTime;
        if(temp > 2.0f)
        {
            DestroyProjectile();
            temp = 0.0f;
        }
    }

    public void FireProjectile(Vector3 _pos, Quaternion _rot)
    {
        temp = 0.0f;
        myTransform.position = _pos;
        myTransform.rotation = _rot;
    }

    public void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }
}

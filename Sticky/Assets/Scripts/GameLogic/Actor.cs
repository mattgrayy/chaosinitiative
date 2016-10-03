using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour
{
    [SerializeField] protected float movementForce = 1000;
    //public ParticleSystem DeathAni;
    protected Transform myTransform = null;
    protected Rigidbody2D ridg = null;

    protected virtual void Awake()
    {
        myTransform = transform;
        ridg = GetComponent<Rigidbody2D>();
    }
}

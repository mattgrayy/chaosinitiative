using UnityEngine;
using System.Collections;

public class VoidProjectile : BasicProjectile
{
    [SerializeField] private float radius=5;

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);

        //RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero)  ;
    }

}
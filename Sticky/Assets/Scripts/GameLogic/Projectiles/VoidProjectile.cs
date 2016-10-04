using UnityEngine;
using System.Collections;

public class VoidProjectile : BasicProjectile
{
    protected override void OnCollisionEnter2D(Collision2D col)
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
            if (col.gameObject.tag == "enemy")
            {
                VoidSuctionManager.instance.GetPooledVoidSuction().StartSuction(transform.position);
                DestroyProjectile();
            }
            else if(col.gameObject.tag == "Shield")
            {
                DestroyProjectile();
            }
        }
    }
}
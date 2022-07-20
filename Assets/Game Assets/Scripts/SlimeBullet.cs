using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBullet : Bullet
{
    protected override void HitTarget()
    {
        target.gameObject.GetComponent<Turret>().TakeDamage(power);
        Destroy(gameObject);
    }
}
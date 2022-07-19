using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Otoma : Turret
{
    public GameObject otomaEffect;
    protected override void Shoot()
    {
        //make AOE ground pound
        Collider[] surroundingObjs = Physics.OverlapSphere(transform.position, range);
        foreach (var collid in surroundingObjs)
        {
            if (collid.CompareTag("Enemy"))
            {
                GameObject effect = (GameObject)Instantiate(otomaEffect, center.position, Quaternion.identity);
                Destroy(effect, 2f);
                collid.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    protected override void FetchPrice()
    {
        damage = 60;
        price = GameObject.Find("Otoma").GetComponent<Card>().price;
    }
    
    protected override void UpgradeSkills()
    {
        range += 0.25f;
        fireRate *= 1.5f;
        damage += 5;
    }
}

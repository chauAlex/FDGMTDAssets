using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Otoma : Turret
{
    public GameObject otomaEffect;
    public GameObject groundEffect;
    protected override void Shoot()
    {
        //make AOE ground pound
        Collider[] surroundingObjs = Physics.OverlapSphere(transform.position, range);
        foreach (var collid in surroundingObjs)
        {
            if (collid.CompareTag("Enemy"))
            {
                StartCoroutine(SpawnParticles(collid));
            }
        }
        
        GetComponent<Animator>().Play("OtomaChildJump");
        GetComponent<Animator>().Play("OtomaChildJump", -1, 0);
    }

    IEnumerator SpawnParticles(Collider collid)
    {
        yield return new WaitForSeconds(1f);
        GameObject effect = (GameObject)Instantiate(otomaEffect, center.position, Quaternion.identity);
        Destroy(effect, 2f);
        GameObject geffect = (GameObject)Instantiate(groundEffect, center.position, Quaternion.Euler(90, 0, 0));
        Destroy(geffect, 2f);
        AudioManager.instance.Play("ThudSound");
        collid.gameObject.GetComponent<Enemy>().TakeDamage(damage);
    }

    protected override void FetchPrice()
    {
        damage = 60;
        price = GameObject.Find("Otoma").GetComponent<Card>().price;
    }
    
    public override void UpgradeSkills()
    {
        range += 0.25f;
        fireRate *= 1.5f;
        damage += 5;
    }
}

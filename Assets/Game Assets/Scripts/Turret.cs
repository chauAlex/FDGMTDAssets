using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Turret : MonoBehaviour
{
    protected Transform target;

    [Header("Attributes")]
    public float range = 2f;
    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public int damage;
    
    [Header("Unity Setup Fields")]
    public Transform center;
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public Vector3 positionOffset;

    protected int price;
    protected int upgradePrice;

    // Start is called before the first frame update
    void Start()
    {
        FetchPrice();
        upgradePrice = price * 2;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    protected virtual void FetchPrice()
    {
        //temporary set damage here
        damage = 40;
        price = GameObject.Find("TestTurret").GetComponent<Card>().price;
    }

    public int GetOriginalPrice()
    {
        return price;
    }

    public int GetUpgradePrice()
    {
        return upgradePrice;
    }
    
    public void IncreaseUpgradePrice()
    {
        upgradePrice *= 2;
    }

    void UpdateTarget()
    {
        //check on a fixed basis, to make sure not to lag out the game
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (var enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(center.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        center.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    protected virtual void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target, damage);
        }
    }

    protected void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        BuildManager.instance.SelectNode(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    protected virtual void UpgradeSkills()
    {
        range += 0.25f;
        fireRate *= 1.5f;
        damage += 5;
    }
}

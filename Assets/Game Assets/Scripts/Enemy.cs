using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Enemy : MonoBehaviour, IPooledObject
{
    public float speed;
    private float origSpeed;
    
    [Header("Debugging Fields")]
    public int health;
    public int strength;
    public float fireRate;
    public float fireCountdown;

    [Header("Adjustable Fields")] 
    public float speedNum = 1f;
    public int healthNum = 100;
    public int strengthNum = 5;
    public float fireRateNum = 0.5f;
    public float fireCountdownNum = 1f;
    public int worth = 50;

    private Transform target;
    private int wavepointIndex;
    private int pathIndex;
    
    public GameObject impactEffect;
    public GameObject slimeBullPrefab;
    public Slider healthSlider;
    public float range = 2f;

    public void OnObjectSpawn()
    {
        speed = speedNum;
        health = healthNum;
        strength = strengthNum;
        fireRate = fireRateNum;
        fireCountdown = fireCountdownNum;
        //choose a path
        Random rnd = new Random();
        pathIndex = rnd.Next(0, Waypoints.Instance.paths);
        target = Waypoints.Instance.points[pathIndex][0];
        origSpeed = speed;
        wavepointIndex = 0;
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
        else
        {
            healthSlider.value = health;
        }
    }

    public void Die()
    {
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 0.65f);
        PlayerStats.instance.money += worth;
        gameObject.SetActive(false);
    }

    public void Slow()
    {
        speed = 0.5f * speed;
        Invoke("ResetSpeed", 5f);
    }
    
    public void SlowTickFor(float factor, float time)
    {
        speed = factor * speed;
        Invoke("ResetSpeed", time);
    }

    private void ResetSpeed()
    {
        speed = origSpeed;
    }

    private void Update()
    {
        if (AudioManager.instance.paused)
        {
            if (fireCountdown <= 0f)
            {
                SearchAndAttack();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
        else
        {
            Vector3 direction = target.position - transform.position;
            if (direction.normalized.x > direction.normalized.y && direction.normalized.x > direction.normalized.z)
                healthSlider.gameObject.SetActive(true);
            else
            {
                healthSlider.gameObject.SetActive(false);
            }
            //move towards waypoint
            transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);
        
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        //check if we are close enough to the waypoint
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            if (wavepointIndex >= Waypoints.Instance.points[pathIndex].Length - 1)
            {
                //has reached the end
                PlayerStats.instance.lives--;
                Die();
                return;
            }
            target = Waypoints.Instance.points[pathIndex][++wavepointIndex];
        }
    }

    private void SearchAndAttack()
    {
        Collider[] surroundingObjs = Physics.OverlapSphere(transform.position, range);
        foreach (var collid in surroundingObjs)
        {
            if (collid.CompareTag("Turret"))
            {
                Transform turrTarget = collid.transform;
                Vector3 direction = turrTarget.position - transform.position;

                Quaternion lookRotation = Quaternion.LookRotation(direction);
                Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
                transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
                //found a target now attack
                GameObject bulletGO = (GameObject)Instantiate(slimeBullPrefab, transform.position, transform.rotation);
                SlimeBullet bullet = bulletGO.GetComponent<SlimeBullet>();

                if (bullet != null)
                {
                    bullet.Seek(turrTarget, strength);
                }

                break;
            }
        }
    }

    private void OnDestroy()
    {
        //here make checks to wavepointIndex
    }
}

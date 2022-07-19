using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Enemy : MonoBehaviour, IPooledObject
{
    public float speed = 1f;
    private float origSpeed;

    public int health;

    private Transform target;
    private int wavepointIndex;
    private int pathIndex;
    
    public GameObject impactEffect;
    

    public void OnObjectSpawn()
    {
        health = 100;
        //choose a path
        Random rnd = new Random();
        pathIndex = rnd.Next(0, Waypoints.Instance.paths);
        target = Waypoints.Instance.points[pathIndex][0];
        origSpeed = speed;
        wavepointIndex = 0;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }

    public void Die()
    {
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 0.65f);
        PlayerStats.instance.money += 50;
        gameObject.SetActive(false);
    }

    public void Slow()
    {
        speed = 0.1f * speed;
        Invoke("ResetSpeed", 5f);
    }

    private void ResetSpeed()
    {
        speed = origSpeed;
    }

    private void Update()
    {
        Vector3 direction = target.position - transform.position;
        //move towards waypoint
        transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);
        
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        
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

    private void OnDestroy()
    {
        //here make checks to wavepointIndex
    }
}

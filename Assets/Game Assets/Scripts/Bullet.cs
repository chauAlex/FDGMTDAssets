using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private int power;

    public float speed = 70f;

    public void Seek(Transform targ, int damage)
    {
        power = damage;
        target = targ;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceInFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceInFrame)
        {
            HitTarget();
            return;
        }
        
        transform.Translate(dir.normalized * distanceInFrame, Space.World);
    }

    void HitTarget()
    {
        target.gameObject.GetComponent<Enemy>().TakeDamage(power);
        Destroy(gameObject);
    }
}

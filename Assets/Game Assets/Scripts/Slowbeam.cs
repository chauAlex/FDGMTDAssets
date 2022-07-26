using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slowbeam : MonoBehaviour
{
    private bool ready;
    private bool lerpColor;
    private Renderer rend;
    private Color startColor;

    private Color colToLerp;
    private bool locked;

    public Image toCharge;

    private float timePassed;

    private Transform parentTrans;

    public int chargeTime;

    private List<Collider> colliders;

    public Collider trueColl;

    public GameObject beamEffect;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        ready = false;
        startColor = rend.material.color;
        colToLerp = Color.white;
        timePassed = 0f;
        parentTrans = transform.parent;
        colliders = new List<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if its ready and constantly update the charge
        if (toCharge.fillAmount < 1)
        {
            colToLerp = Color.white;
            toCharge.color = colToLerp;
            if (!AudioManager.instance.paused)
            {
                timePassed += Time.deltaTime;
                toCharge.fillAmount = (timePassed / chargeTime > 1) ? 1 : timePassed / chargeTime;   
            }
        }
        else
        {
            ready = true;
            colToLerp = Color.green;
            toCharge.color = colToLerp;

        }
        
        if (lerpColor)
        {
            rend.material.color = Color.Lerp(startColor, colToLerp, Mathf.PingPong(Time.time, 1));
        }
        else
        {
            rend.material.color = startColor;
        }
    }

    private void OnMouseDown()
    {
        RaycastHit[] hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics.RaycastAll(ray);
        bool actualHit = false;
        foreach (var hits in hit)
        {
            if (hits.collider == trueColl)
                actualHit = true;
        }
        if (!ready || !actualHit)
            return;
        ready = false;
        timePassed = 0f;
        toCharge.fillAmount = 0f;
        Stun();
    }

    private void Stun()
    {
        GameObject effect = (GameObject)Instantiate(beamEffect, transform.position, parentTrans.rotation);
        Destroy(effect, 2f);
        locked = true;
        foreach (var collid in colliders)
        {
            if (collid.CompareTag("Enemy"))
            {
                //TODO: add effect here
                collid.gameObject.GetComponent<Enemy>().Slow();
                //make them take about 10 damage
                collid.gameObject.GetComponent<Enemy>().TakeDamage(80);
            }
        }
        colliders.Clear();
        locked = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!locked)
        {
            if (!colliders.Contains(other))
            {
                colliders.Add(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!locked)
        {
            if (colliders.Contains(other))
            {
                colliders.Remove(other);
            }
        }
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        RaycastHit[] hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics.RaycastAll(ray);
        foreach (var hits in hit)
        {
            if (hits.collider == trueColl)
                lerpColor = true;
            return;
        }
    }

    private void OnMouseExit()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        lerpColor = false;
    }
/* Obsolete Code:
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        
        Gizmos.DrawWireCube(transform.position + 4*transform.parent.forward, transform.parent.up + transform.parent.right + 7*transform.parent.forward);
    }
    */
}

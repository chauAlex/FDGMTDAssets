using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = true;
    public float panSpeed = 30f;
    public float panBorder = 10f;
    public float scrollSpeed = 5f;
    public float minY = 2.5f;
    public float maxY = 8f;
    private Queue<GameObject> reAppear = new Queue<GameObject>();
    //private bool hasObstacle = false;
    //public Material regCarousel;
    //public Material carouselClearMat;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            doMovement = !doMovement;
        if(!doMovement)
            return;
        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.right * (panSpeed * Time.deltaTime), Space.World);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.left * (panSpeed * Time.deltaTime), Space.World);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.back * (panSpeed * Time.deltaTime), Space.World);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.forward * (panSpeed * Time.deltaTime), Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
/*
        //raycast downward specifically for level 2
        if (!hasObstacle)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
            {
                //get the hit distance from the center of the hit transform
                float distance = (hit.point - hit.collider.bounds.center).magnitude;
                reAppear.Enqueue(hit.transform.gameObject);
                //divide distance by radius and make it the factor to change the alpha of the material
                Renderer rend = hit.transform.gameObject.GetComponent<Renderer>();
                rend.material = carouselClearMat;
                hasObstacle = true;
            }
        }
        else
        {
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
            {
                Renderer rend = reAppear.Dequeue().GetComponent<Renderer>();
                rend.material = regCarousel;
                hasObstacle = false;
            }
        }
        */
    }
}

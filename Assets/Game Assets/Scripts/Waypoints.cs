using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Transform[][] points;
    public int paths;
    public int[] breakpoints;
    
    public static Waypoints Instance;
    
    private void Awake()
    {
        if(Instance != null)
            Debug.LogWarning("MORE THAN ONE WAYPOINTS");
        Instance = this;
        
        int breakIndex = 0;
        int decrementor = 0;
        points = new Transform[paths][];
        //preallocate the array
        int prevIndex = 0;
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = new Transform[breakpoints[i] - prevIndex];
            prevIndex = breakpoints[i] - prevIndex;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            if (breakIndex < breakpoints.Length && i == breakpoints[breakIndex])
            {
                breakIndex++;
                decrementor += i;
            }
            points[breakIndex][ i - decrementor] = transform.GetChild(i);
        }
    }
}

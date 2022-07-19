using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject spawnpointGO;
    public static BuildManager instance;
    public int currPrice;

    private GameObject turretToBuild;
    private GameObject selectedTurr;

    public NodeUI nodeUI;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("error - more than one buildmanager");
            return;
        }
        instance = this;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SelectNode(GameObject targTurret)
    {
        if (selectedTurr == targTurret)
        {
            DeselectNode();
            return;
        }
        selectedTurr = targTurret;
        turretToBuild = null;
        
        nodeUI.SetTarget(targTurret);
    }

    public void DeselectNode()
    {
        selectedTurr = null;
        nodeUI.Hide();
    }

    public void SetTurretToBuild(GameObject inTurret)
    {
        turretToBuild = inTurret;
        DeselectNode();
    }
    
}

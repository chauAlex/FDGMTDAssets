using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    private GameObject turret;

    private Renderer rend;
    private Color startColor;
    private bool mouseDown;
    
    // Start is called before the first frame update
    void Start()
    {
        mouseDown = false;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void Update()
    {
        flucAlpha(rend.material, Mathf.Abs(Mathf.Sin(Time.time * 2)));
        if (Input.GetMouseButtonDown(0) && !mouseDown)
        {
            BuildManager.instance.currPrice = 0;
            BuildManager.instance.spawnpointGO.SetActive(false);
        }
    }

    private void flucAlpha(Material mat, float alphaVal)
    {
        Color old = mat.color;
        Color newColor = new Color(old.r, old.g, old.b, alphaVal);
        mat.color = newColor;
    }

    private void OnMouseDown()
    {
        mouseDown = true;
        if (turret != null)
        {
            //this node has been selected for upgrade/sell
            BuildManager.instance.SelectNode(turret);
        }
        else
        {
            //subtract the money
            PlayerStats.instance.money -= BuildManager.instance.currPrice;
            BuildManager.instance.currPrice = 0;
            AudioManager.instance.Play("DroppedDown");
            //Place down the turret
            GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
            positionOffset = turretToBuild.GetComponent<Turret>().positionOffset;
            turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
            mouseDown = false;
        }
        BuildManager.instance.spawnpointGO.SetActive(false);
    }

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}

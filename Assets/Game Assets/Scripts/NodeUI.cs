using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    [Header("To Change Each Click")]
    private GameObject turretGO;
    private Turret turret;

    public GameObject UI;

    public GameObject upgradeButtonGO;
    public GameObject sellButtonGO;

    private Button upgradeButton;
    private Button sellButton;
    private Color origColor;

    private TextMeshProUGUI upgradeText;
    private TextMeshProUGUI sellText;

    private void Awake()
    {
        upgradeButton = upgradeButtonGO.GetComponent<Button>();
        origColor = upgradeButton.GetComponent<Image>().color;
        sellButton = sellButtonGO.GetComponent<Button>();
        upgradeText = upgradeButtonGO.GetComponentInChildren<TextMeshProUGUI>();
        sellText = sellButtonGO.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetTarget(GameObject targTurret)
    {
        turretGO = targTurret;

        transform.position = turretGO.transform.position;
        turret = turretGO.GetComponent<Turret>();

        UpdateText();
        
        UI.SetActive(true);
    }

    public void Hide()
    {
        UI.SetActive(false);
        turretGO = null;
        turret = null;
    }

    private void UpdateText()
    {
        upgradeText.text = $"Upgrade\n${turret.GetUpgradePrice()}";
        sellText.text = $"Sell\n+${turret.GetOriginalPrice()/2}";
    }

    public void Upgrade()
    {
        //take out money and increase next time price
        PlayerStats.instance.money -= turret.GetUpgradePrice();
        turret.IncreaseUpgradePrice();
        
        UpdateText();
        turret.UpgradeSkills();
    }

    public void Remove()
    {
        //give money and increase next time price
        PlayerStats.instance.money += turret.GetOriginalPrice()/2;
        AudioManager.instance.Play("SellSound");
        
        Destroy(turretGO);
        
        Hide();
    }

    private void Update()
    {
        if (UI.activeSelf)
        {
            if (PlayerStats.instance.money < turret.GetUpgradePrice())
            {
                //make the upgrade button disabled
                upgradeButton.enabled = false;
                upgradeButton.GetComponent<Image>().color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
            }
            else
            {
                upgradeButton.enabled = true;
                upgradeButton.GetComponent<Image>().color = new Color(origColor.r, origColor.g, origColor.b, origColor.a);
            }
        }
    }
}

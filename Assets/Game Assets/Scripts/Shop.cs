using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public void PurchaseTurret(Card card)
    {
        AudioManager.instance.Play("ClickedButton");
        BuildManager.instance.currPrice = card.price;
        BuildManager.instance.SetTurretToBuild(card.prefab);
        BuildManager.instance.spawnpointGO.SetActive(true);
    }
}

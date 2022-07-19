using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public int startMoney;
    public int startLives;

    public int money;
    public int lives;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI livesText;
    private void Start()
    {
        money = startMoney;
        lives = startLives;
        moneyText.text = money.ToString();
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("error - more than one playerstats");
            return;
        }
        instance = this;
    }

    private void Update()
    {
        moneyText.text = money.ToString();
        livesText.text = lives.ToString() + " Lives";
    }
}

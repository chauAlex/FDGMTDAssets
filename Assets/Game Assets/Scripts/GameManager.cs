using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool gameEnded = false;

    public GameObject gameOverUI;

    public GameObject gameWonUI;

    public Slider musicTime;
    public TextMeshProUGUI timeFloat;

    public string nextLevel = "Level02";
    public float levelTime = 60f;
    public int nextLevelIndex = 2;
    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
            return;
        
        //update audio slider
        musicTime.value = AudioManager.instance.GetTime("MainTheme");
        timeFloat.text = Math.Round((decimal)AudioManager.instance.GetTime("MainTheme"), 2).ToString();
        
        if (PlayerStats.instance.lives <= 0)
        {
            EndGame();
        }
        else if (AudioManager.instance.GetTime("MainTheme") >= levelTime)
        {
            WonGame();
        }

        if (AudioManager.instance.GetTime("SlimeAttackTheme") >= 20f)
            AudioManager.instance.DisableButton();
    }

    private void EndGame()
    {
        gameEnded = true;
        AudioManager.instance.StopAll();
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }
    
    private void WonGame()
    {
        gameEnded = true;
        AudioManager.instance.StopAll();
        Time.timeScale = 0f;
        PlayerPrefs.SetInt("levelReached", nextLevelIndex);
        gameWonUI.SetActive(true);
    }
}

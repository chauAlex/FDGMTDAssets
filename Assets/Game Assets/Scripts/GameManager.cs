using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool gameEnded = false;

    public GameObject gameOverUI;

    public GameObject gameWonUI;

    public Slider musicTime;

    public string nextLevel = "Level02";

    public int nextLevelIndex = 2;
    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
            return;
        
        //update audio slider
        musicTime.value = AudioManager.instance.GetTime("MainTheme");
        
        if (PlayerStats.instance.lives <= 0)
        {
            EndGame();
        }
        else if (AudioManager.instance.GetTime("MainTheme") >= 60f)
        {
            WonGame();
        }

        if (AudioManager.instance.GetTime("SlimeAttackTheme") >= 8.5f)
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    public SceneFader sf;
    public GameObject ui;
    public void Retry()
    {
        sf.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sf.FadeTo("MainMenu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
            Time.timeScale = 0f;
        else
        {
            Time.timeScale = 1f;
        }
        
        AudioManager.instance.ChangeThemeState();
    }
}
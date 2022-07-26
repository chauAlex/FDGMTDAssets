using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public SceneFader sf;
    public GameObject ui;
    public Slider volChange;
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

    public void VolumeToggle()
    {
        AudioManager.instance.DecVolumeAll(volChange.value);
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
            AudioManager.instance.pausedUI = true;
        }
        else
        {
            Time.timeScale = 1f;
            AudioManager.instance.pausedUI = false;
        }
        
        AudioManager.instance.PauseMenuChangeState();
    }
}
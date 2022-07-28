using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComingSoon : MonoBehaviour
{
    public string levelToLoad = "MainMenu";
    public SceneFader sceneFader;
    public GameObject musicManager;

    public void Home()
    {
        AudioManager.instance.Play("ClickedButton");
        sceneFader.FadeTo(levelToLoad);
    }

    public void Quit()
    {
        AudioManager.instance.Play("ClickedButton");
        Application.Quit();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "TestLevel";
    public SceneFader sceneFader;
    public GameObject musicManager;

    public void Play()
    {
        AudioManager.instance.Play("ClickedButton");
        DontDestroyOnLoad(musicManager);
        sceneFader.FadeTo(levelToLoad);
    }

    public void Quit()
    {
        AudioManager.instance.Play("ClickedButton");
        Application.Quit();
    }
}

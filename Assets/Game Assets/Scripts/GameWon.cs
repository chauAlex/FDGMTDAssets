using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWon : MonoBehaviour
{
    public SceneFader sf;
    public GameManager gm;
    public void NextLevel()
    {
        Time.timeScale = 1f;
        sf.FadeTo(gm.nextLevel);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        sf.FadeTo("MainMenu");
    }
}
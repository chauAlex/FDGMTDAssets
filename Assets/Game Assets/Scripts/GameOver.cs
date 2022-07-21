using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public SceneFader sf;
    public void Retry()
    {
        sf.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sf.FadeTo("MainMenu");
    }
}

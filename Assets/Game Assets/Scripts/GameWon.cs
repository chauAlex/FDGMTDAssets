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
        sf.FadeTo(gm.nextLevel);
    }

    public void Menu()
    {
        sf.FadeTo("MainMenu");
    }
}
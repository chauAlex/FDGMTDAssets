using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public SceneFader sf;
    public Button[] levelButtons;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
                levelButtons[i].GetComponentInChildren<TextMeshProUGUI>().alpha = 42f/255f;
            }
        }
    }

    public void Select(string levelName)
    {
        sf.FadeTo(levelName);
    }
}

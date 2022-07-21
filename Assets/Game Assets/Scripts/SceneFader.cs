using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public Image img;
    private Color origColor;
    public AnimationCurve curve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private void Awake()
    {
        origColor = img.color;
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
        
    }

    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(origColor.r, origColor.g, origColor.b, t);
            yield return 0;
        }
    }
    
    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(origColor.r, origColor.g, origColor.b, t);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}

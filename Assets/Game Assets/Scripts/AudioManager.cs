using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Toggle = UnityEngine.UIElements.Toggle;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    public bool paused;
    public Image buttonImage;
    public Animator buttonAnim;
    public bool pausedUI;
    public Button playButton;
    private bool toggle;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
        
        Play("MainTheme");
        paused = false;
        pausedUI = false;
        toggle = false;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }
    
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Stop();
    }

    public float GetTime(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s.source.time;
    }

    public void StopAll()
    {
        foreach (var s in sounds)
        {
            s.source.Stop();
        }
    }
    public void DisableButton()
    {
        if(toggle)
            return;
        else
        {
            toggle = true;
        }
        ChangeThemeState();
        buttonImage.color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
        buttonAnim.enabled = false;
        playButton.enabled = false;
    }

    public void ChangeThemeState()
    {
        if (paused)
        {
            ToggleMainAndAttackTheme();
            paused = false;
            buttonImage.color = Color.white;
            buttonAnim.enabled = true;
        }
        else
        {
            ToggleMainAndAttackTheme();
            paused = true;
            buttonImage.color = Color.red;
            buttonAnim.enabled = false;
        }
    }

    public void PauseMenuChangeState()
    {
        Sound s = Array.Find(sounds, sound => sound.name == "MainTheme");
        Sound pTheme = Array.Find(sounds, sound => sound.name == "SlimeAttackTheme");
        if (pausedUI)
        {
            if(paused)
                pTheme.source.Pause();
            else
            {
                s.source.Pause();
            }
        }
        else
        {
            if(paused)
                pTheme.source.UnPause();
            else
            {
                s.source.UnPause();
            }
        }
    }
    private void ToggleMainAndAttackTheme()
    {
        Sound s = Array.Find(sounds, sound => sound.name == "MainTheme");
        Sound pTheme = Array.Find(sounds, sound => sound.name == "SlimeAttackTheme");
        if (paused)
        {
            s.source.UnPause();
            pTheme.source.Pause();
        }
        else
        {
            pTheme.source.Play();
            s.source.Pause();
        }
    }

    public void DecVolumeAll(float value)
    {
        //Debug.Log("adjusting vol by factor of " + value);
        foreach (var s in sounds)
        {
            //adjusts the volume off 0-1 scale, for the pause menu specifically
            s.source.volume = s.volume * value;
        }
    }
}

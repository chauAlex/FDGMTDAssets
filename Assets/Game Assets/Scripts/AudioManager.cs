using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    public bool paused;
    public Image buttonImage;
    public Animator buttonAnim;
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
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        Debug.Log("Attempting to play " + name);
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

    public void ChangeThemeState()
    {
        Sound s = Array.Find(sounds, sound => sound.name == "MainTheme");
        if (paused)
        {
            s.source.UnPause();
            paused = false;
            buttonImage.color = Color.white;
            buttonAnim.enabled = true;
        }
        else
        {
            s.source.Pause();
            Play("PausedMusic");
            paused = true;
            buttonImage.color = Color.red;
            buttonAnim.enabled = false;
        }
    }

    public void DecVolumeAll(float value)
    {
        Debug.Log("adjusting vol by factor of " + value);
        foreach (var s in sounds)
        {
            //adjusts the volume off 0-1 scale, for the pause menu specifically
            s.source.volume = s.volume * value;
        }
    }
}

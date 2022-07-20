using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PPVolumeAdjuster : MonoBehaviour
{
    private VolumeProfile vp;
    private Vignette vig;
    private void Start()
    {
        vp = GetComponent<Volume>().profile;
        if (!vp) throw new NullReferenceException(nameof(UnityEngine.Rendering.Volume));
        
        if(!vp.TryGet(out vig)) throw new System.NullReferenceException(nameof(vig));
    }

    private void Update()
    {
        if (AudioManager.instance.paused)
        {
            vig.intensity.value = Mathf.Lerp(vig.intensity.value, 0.65f, 1 * Time.deltaTime);
        }
        else
        {
            if(Math.Abs(vig.intensity.value - 0.25f) > 0.001)
                vig.intensity.value = Mathf.Lerp(vig.intensity.value, 0.25f, 1 * Time.deltaTime);
        }
    }
}
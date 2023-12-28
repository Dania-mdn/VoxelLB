using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facel : MonoBehaviour
{
    public ParticleSystem particle;
    public Light light;

    private void OnEnable()
    {
        EventSystem.Night += SetNight;
    }
    private void OnDisable()
    {
        EventSystem.Night -= SetNight;
    }

    private void SetNight(bool isNight)
    {
        if(isNight)
        {
            if(particle.isPlaying == false)
            {
                particle.Play();
                light.enabled = true;
            }
        }
        else
        {
            particle.Stop();
            light.enabled = false;
        }
    }
}

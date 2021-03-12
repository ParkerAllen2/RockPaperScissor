using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    AudioSource[] audioSources;
    float[] volumes;
    [SerializeField] Slider slider;

    public void Start()
    {
        audioSources = GetComponentsInChildren<AudioSource>();
        volumes = new float[audioSources.Length];

        for(int i = 0; i < audioSources.Length; i++)
        {
            volumes[i] = audioSources[i].volume;
        }
    }

    public void UpdateVolumes()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = volumes[i] * slider.value;
        }
    }
}

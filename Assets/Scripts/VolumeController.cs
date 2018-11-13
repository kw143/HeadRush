using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;
    public float proportion;
    public AudioSource musicAudio;
    public AudioSource[] sfx;
    public float[] sfxProp;
    public static float maxVol = 1;

    // Use this for initialization
    void Start()
    {
        if (musicAudio != null)
        {
            musicAudio.volume = proportion * maxVol;
        }
        int i = 0;
        foreach (AudioSource soundfx in sfx)
        {
            soundfx.volume = (sfxProp[i]) * maxVol;
            i++;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void ChangeVol() {
        maxVol = volumeSlider.value;
        musicAudio.volume = 0.653f * maxVol;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer Mixer;
    public string AudioMixerName;


    public void SetLevel (float sliderVal)
    {
        Mixer.SetFloat(AudioMixerName,Mathf.Log10(sliderVal)*20);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundButton : MonoBehaviour
{
    public AudioSource Sound;
    public void PlaySound()
    {
        Sound.Play();
    }
}

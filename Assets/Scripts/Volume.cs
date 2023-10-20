using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
///So we can use the audio methods

public class Volume : MonoBehaviour
{
    public AudioMixer audioMixer;
    ///References our mixer

    public void SetVolume(float volume)
    ///A float is given by the value on the slider
    {
        audioMixer.SetFloat("Volume", volume);
        ///The parameter on the audio mixer that is Volume is set to the value of volume
    }
}

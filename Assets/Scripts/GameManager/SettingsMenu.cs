using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mixer;

    public void VolumeControl(float volume)
    {
        mixer.SetFloat("volume", volume);
    }
}

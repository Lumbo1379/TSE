using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer soundTrackMixer;
    public AudioMixer SFXmixer;

    public void VolumeControl(float volume)
    {
        soundTrackMixer.SetFloat("volume", volume);
        
    }

    public void SFXControl(float volume)
    {
        SFXmixer.SetFloat("volume", volume);
    }
}

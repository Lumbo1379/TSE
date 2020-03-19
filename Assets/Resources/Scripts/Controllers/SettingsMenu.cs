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

    //sound affects by default will be quieter than the soundtrack
    public void SFXControl(float volume)
    {
        SFXmixer.SetFloat("volume", volume);
    }
}

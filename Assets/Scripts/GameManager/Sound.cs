using UnityEngine.Audio;
using UnityEngine;


//custom class, so to ensure seen in inspector, needs to be serialised
[System.Serializable]

public class Sound
{

    public string name;


    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    public bool loop;

    [HideInInspector]
    public AudioSource src;

}



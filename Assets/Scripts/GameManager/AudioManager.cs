using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        

        //DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.src = gameObject.AddComponent<AudioSource>();
            s.src.clip = s.clip;
            s.src.loop = s.loop;

            //volume control
            s.src.volume = s.volume;
            s.src.outputAudioMixerGroup = s.group;
        }
    }

    private void Start()
    {
        PlayAudio("Menu");
    }

    public void PlayAudio(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.src.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //sets volume of music to 0 when game paused, allows music to then continue when resumed
        s.src.volume = 0;
    }
    //called when game is resumed after pause
    public void Resume(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.src.volume = s.volume;
    }

    


}

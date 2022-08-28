using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioController : MonoBehaviour
{
    public static AudioController audioControllerInstance;

    public sound[] sounds;
    private PlayerController thePlayer;

    void Awake()
    {
        if (audioControllerInstance == null)
        {
            audioControllerInstance = this;
        }
        foreach (var s in sounds)
        {
            thePlayer = FindObjectOfType<PlayerController>();
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    public void Stop(string name)
    {
        sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
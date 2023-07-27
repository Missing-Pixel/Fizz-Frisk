using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;
using JetBrains.Annotations;
using Random = UnityEngine.Random;

public class SCR_AudioManager : MonoBehaviour
{
    public SCR_Sound[] sounds;
    public static SCR_AudioManager instanceFix;

    private int temp;

    private void Awake()
    {
        if (instanceFix == null)
        {
            instanceFix = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (SCR_Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlaySounds(string name)
    {
        SCR_Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void PlaySoundsVaried(string name)
    {
        SCR_Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.pitch += Random.Range(-0.1f,0.5f);
        s.source.Play();
    }

    public void StopSounds(string name)
    {
        SCR_Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }
}

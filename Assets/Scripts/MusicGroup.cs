using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller of a music group (for music benches). <br/>
/// Provider for MusicConsumer child objects.
/// </summary>
public class MusicGroup : MonoBehaviour
{
    public enum FlipModifier
    {
        NORMAL = 0,
        VFLIP = 1,
        HFLIP = 2,
        VHFLIP = 3,
    }

    // beats per minute
    public float tempo = 60.0f;

    public AudioClip normalSource;
    public AudioClip vFlipSource;
    public AudioClip hFlipSource;
    public AudioClip vHFlipSource;

    private readonly List<MusicConsumer> consumers = new List<MusicConsumer>();

    public AudioClip GetMusicSource(FlipModifier modifier)
    {
        switch(modifier)
        {
            case FlipModifier.NORMAL:
                return normalSource;

            case FlipModifier.VFLIP:
                return vFlipSource;

            case FlipModifier.HFLIP:
                return hFlipSource;

            case FlipModifier.VHFLIP:
                return vHFlipSource;

            default:
                throw new ArgumentException("invalid modifier");
        }
    }

    public void PlayAllMusic()
    {
        foreach (MusicConsumer consumer in consumers)
        {
            consumer.PlayMusic();
        }
    }

    public void PauseAllMusic()
    {
        foreach (MusicConsumer consumer in consumers)
        {
            consumer.PauseMusic();
        }
    }

    public void StopAllMusic()
    {
        foreach (MusicConsumer consumer in consumers)
        {
            consumer.StopMusic();
        }
    }

    public void AddConsumer(MusicConsumer consumer)
    {
        consumers.Add(consumer);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(normalSource != null, "normal source should be available");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

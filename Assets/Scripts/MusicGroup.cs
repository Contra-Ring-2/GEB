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
    // beats per minute
    public float tempo = 60.0f;

    public AudioClip normalSource;
    public AudioClip vFlipSource;
    public AudioClip hFlipSource;
    public AudioClip vHFlipSource;

    public enum FlipModifier
    {
        NORMAL = 0,
        VFLIP = 1,
        HFLIP = 2,
        VHFLIP = 3,
    }

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

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(normalSource != null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

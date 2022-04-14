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

    public AudioSource normalSource;
    public AudioSource vFlipSource;
    public AudioSource hFlipSource;
    public AudioSource vHFlipSource;

    public enum FlipModifier
    {
        NORMAL = 0,
        VFLIP = 1,
        HFLIP = 2,
        VHFLIP = 3,
    }

    /// <summary>
    /// Play music with specified modifier after specific beats
    /// </summary>
    /// <param name="modifier">flip modifier</param>
    /// <param name="beats">interval beats</param>
    public void PlayMusicAfter(FlipModifier modifier, float beats)
    {
        float seconds = (60/tempo) * beats;

        MasterModel.TheModel.CallbackInSecond(
            seconds,
            () =>
                {
                    GetMusicSource(modifier).Play();
                }
        );
    }

    public AudioSource GetMusicSource(FlipModifier modifier)
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

    public void ConfigureConsumer(MusicConsumer consumer)
    {

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

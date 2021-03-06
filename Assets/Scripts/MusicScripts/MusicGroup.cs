using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Controller of a music group (for music benches). <br/>
/// Provider for MusicConsumer child objects.
/// </summary>
public class MusicGroup : MonoBehaviour
{
    // RingControl
    public class Note
    {
        public float start_time;
        public float end_time;

        // moechine said she spell this intentionally :)
        public float hieght; // radius

        public override string ToString()
        {
            //return base.ToString();
            return string.Format(
                    "Note[start: {0}, end: {1}, hieght: {2}]",
                    start_time,
                    end_time,
                    hieght
                );
        }
    }

    public enum FlipModifier : uint
    {
        NORMAL = 0,
        VFLIP = 1,
        HFLIP = 2,
        VHFLIP = 3,
    }

    // master mixer group (e.g. Default > Master)
    public AudioMixerGroup masterMixerGroup;

    // beats per minute
    public float tempo = 60.0f;

    public AudioClip normalSource;
    public AudioClip vFlipSource;
    public AudioClip hFlipSource;
    public AudioClip vHFlipSource;

    public TextAsset normalMusicXML;
    public float vFlipOffset;

    private readonly List<MusicConsumer> consumers = new List<MusicConsumer>();

    public Note[] GetAllNotes()
    {
        List<Note> allNotes = new List<Note>();
        for (int i = 0; i < consumers.Count; i++)
        {
            MusicConsumer consumer = consumers[i];
            allNotes.AddRange(consumer.GetNotes(i));
        }

        return allNotes.ToArray();
    }

    public AudioMixerGroup GetMixerGroup(int keyShift)
    {
        // TODO: optimize this
        string targetName = string.Format("Music{0}", keyShift);

        AudioMixerGroup[] mixers = masterMixerGroup.audioMixer.FindMatchingGroups(targetName);
        Debug.Assert(mixers != null && mixers.Length > 0, string.Format("cannot find mixer {0}", targetName));

        if (mixers.Length > 1)
        {
            Debug.LogWarning(string.Format("found more than one matching mixer of '{0}': {1}", targetName, mixers));
        }
        return mixers[0];
    }

    public AudioClip GetMusicSource(FlipModifier modifier)
    {
        switch (modifier)
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
        //Debug.Log("All scores : " + GetAllNotes());
        //foreach (Note note in GetAllNotes())
        //{
        //    Debug.Log("scores: " + string.Join(",", note));
        //}

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
        //Debug.Assert(defaultMixerGroup != null, "mixer group should be available (could be [Default > Master])");

    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Playable music comsumer. <br/>
/// It needs a parent MusicGroup object. <br/>
/// </summary>
public class MusicConsumer : MonoBehaviour
{
    public MusicGroup.FlipModifier modifier = MusicGroup.FlipModifier.NORMAL;
    public float waitBeats = 0.0f;
    
    public int keyShift = 0;
    public float speedMultiplier = 1.0f;

    public bool isEnabled = false;

    // TODO: speed, pitch?

    private MusicGroup musicGroup;

    /// <summary>
    /// Play music with consumer specified argument
    /// </summary>
    public void PlayMusic()
    {
        if (!isEnabled)
        {
            return;
        }

        AudioSource source = GetComponent<AudioSource>();
        float seconds = (60 / musicGroup.tempo) * waitBeats;

        source.clip = musicGroup.GetMusicSource(modifier);
        source.outputAudioMixerGroup = musicGroup.GetMixerGroup(keyShift);

        source.PlayDelayed(seconds);

        //MasterModel.TheModel.CallbackInSecond(
        //    seconds,
        //    () =>
        //    {
        //        //source.clip = musicGroup.GetMusicSource(modifier);

        //        //Debug.Log("Playing music: " + source.clip);
        //        source.Play();
        //    }
        //);
    }

    public void PauseMusic()
    {
        GetComponent<AudioSource>().Pause();
    }

    public void StopMusic()
    {
        GetComponent<AudioSource>().Stop();
    }

    public MusicGroup.Note[] GetNotes()
    {
        List<MusicGroup.Note> notes = new List<MusicGroup.Note>();
        if (musicGroup.normalMusicXML == null)
        {
            // two tiger
            float[] tigerNotes =
            {
                1, 1,
                2, 1,
                3, 1,
                1, 1,

                1, 1,
                2, 1,
                3, 1,
                1, 1,

                3,   1,
                4,   1,
                5,   2,

                3,   1,
                4,   1,
                5,   2,

                5, .5F,
                6, .5F,
                5, .5F,
                4, .5F,
                3,   1,
                1,   1,

                5, .5F,
                6, .5F,
                5, .5F,
                4, .5F,
                3,   1,
                1,   1,

                 1, 1,
                -2, 1,
                 1, 2,

                 1, 1,
                -2, 1,
                 1, 2,
            };

            {
                float sumTime = 0;
                for (int i = 0; i < tigerNotes.Length; i += 2)
                {
                    float height = tigerNotes[i];
                    float duration = tigerNotes[i + 1];

                    float startTime = sumTime;
                    float endTime = startTime + duration;

                    notes.Add(new MusicGroup.Note { start_time = startTime, end_time = endTime, hieght = height });
                }
            }
        }

        return notes.ToArray();
    }

    // Start is called before the first frame update
    void Start()
    {
        musicGroup = transform.parent.GetComponent<MusicGroup>();
        Debug.Assert(musicGroup != null, "MusicCosumer needs a parent MusicGroup object");

        //if (targetTempo == 0.0f)
        //{
        //    targetTempo = musicGroup.tempo;
        //}

        // configure consumer
        gameObject.AddComponent<AudioSource>();
        //GetComponent<AudioSource>().outputAudioMixerGroup = Instantiate(musicGroup.defaultMixerGroup);
        //GetComponent<AudioSource>().outputAudioMixerGroup = musicGroup.defaultMixerGroup;

        musicGroup.AddConsumer(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

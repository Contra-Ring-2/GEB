using System;
using System.Collections;
using System.Collections.Generic;
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
    
    public float keyShift = 0.0f;
    public float speedMultiplier = 1.0f;

    // TODO: speed, pitch?

    private MusicGroup musicGroup;

    /// <summary>
    /// Play music with consumer specified argument
    /// </summary>
    public void PlayMusic()
    {
        AudioSource source = GetComponent<AudioSource>();
        float seconds = (60 / musicGroup.tempo) * waitBeats;

        source.clip = musicGroup.GetMusicSource(modifier);

        //Debug.Assert(source.outputAudioMixerGroup != null, "1");
        //Debug.Assert(source.outputAudioMixerGroup.audioMixer != null, "2");

        // speedMultiplier
        AudioMixer mixer = source.outputAudioMixerGroup.audioMixer;

        float defaultPitch = 1.0f;
        mixer.GetFloat("Pitch", out defaultPitch);

        float pitch = defaultPitch * (float) Math.Pow(2, keyShift);
        mixer.SetFloat("Pitch", pitch);

        Debug.Log(string.Format("{0} set pitch: {1} -> {2}", gameObject, defaultPitch, pitch));

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
        GetComponent<AudioSource>().outputAudioMixerGroup = Instantiate(musicGroup.defaultMixerGroup);
        //GetComponent<AudioSource>().outputAudioMixerGroup = musicGroup.defaultMixerGroup;

        musicGroup.AddConsumer(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

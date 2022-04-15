using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playable music comsumer. <br/>
/// It needs a parent MusicGroup object. <br/>
/// </summary>
public class MusicConsumer : MonoBehaviour
{
    public MusicGroup.FlipModifier modifier;
    public float waitBeats;

    private MusicGroup musicGroup;

    /// <summary>
    /// Play music with consumer specified argument
    /// </summary>
    public void PlayMusic()
    {
        AudioSource source = GetComponent<AudioSource>();
        float seconds = (60 / musicGroup.tempo) * waitBeats;

        MasterModel.TheModel.CallbackInSecond(
            seconds,
            () =>
            {
                source.clip = musicGroup.GetMusicSource(modifier);
                source.Play();
            }
        );
    }

    // Start is called before the first frame update
    void Start()
    {
        musicGroup = transform.parent.GetComponent<MusicGroup>();
        Debug.Assert(musicGroup != null, "MusicCosumer needs a parent MusicGroup object");

        // configure consumer
        gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

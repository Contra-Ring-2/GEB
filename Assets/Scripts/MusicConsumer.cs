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
    public float waitDuration;

    private MusicGroup musicGroup;

    // Start is called before the first frame update
    void Start()
    {
        musicGroup = transform.parent.GetComponent<MusicGroup>();
        Debug.Assert(musicGroup != null, "MusicCosumer needs a parent MusicGroup object");

        musicGroup.ConfigureConsumer(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

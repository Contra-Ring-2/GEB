using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays the music during trigger
/// </summary>
public class PlayerMusicAtTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(this + ": triggerEnter");
        GetComponent<MusicGroup>().PlayAllMusic();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(this + ": triggerExit");
        GetComponent<MusicGroup>().PauseAllMusic();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(GetComponent<MusicGroup>() != null, "need a MusicGroup to play");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

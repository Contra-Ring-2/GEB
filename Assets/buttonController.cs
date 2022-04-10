using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonController : MonoBehaviour
{
    private AudioSource audiomv;
    private ParticleSystem ring;
    public void Start()
    {
        audiomv = GetComponent<AudioSource>();
        ring = GameObject.Find("ring").GetComponent<ParticleSystem>();
        ring.Stop();
}
    public void Play(AudioSource audioData)
    {
        if (audioData != null)
        {
            audioData.Play(0);
        }
    }
    private void OnGUI()
    {
        GUI.Box(new Rect(70, 40, 80, 80), "Menu");
        if (GUI.Button(new Rect(80, 60, 65, 15), "Play(f1)") || Input.GetKey("f1"))
        {
            Debug.Log("press play");
            ring.Play();
            audiomv.Play();

        }
        if (GUI.Button(new Rect(80, 80, 65, 15), "Pause(f2)") || Input.GetKey("f2"))
        {
            Debug.Log("press pause");
            ring.Pause();
            audiomv.Pause();

        }
    }
}

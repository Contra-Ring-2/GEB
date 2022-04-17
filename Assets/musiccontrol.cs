using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musiccontrol : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource music1;
    private AudioSource music2;
    private ParticleSystem ring1;
    private ParticleSystem ring2;
    void Start()
    {
        music1 = GameObject.Find("music1").GetComponent<AudioSource>();
        music2 = GameObject.Find("music2").GetComponent<AudioSource>();
        ring1 = GameObject.Find("ring1").GetComponent<ParticleSystem>();
        ring2 = GameObject.Find("ring2").GetComponent<ParticleSystem>();
        ring1.Stop();
        ring2.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F2))
        {
            Debug.Log("press play1");
            ring1.Play();
            music1.Play();
        }
        if (Input.GetKey(KeyCode.F3))
        {
            Debug.Log("press play2");
            ring2.Play();
            music2.Play();
        }

    }
    private void OnGUI()
    {
        GUI.Box(new Rect(160, 80, 100, 100), "Menu");
        if (GUI.Button(new Rect(180, 110, 60, 20), "Play"))
        {
            Debug.Log("press play1");
            ring1.Play();
            music1.Play();

        }
        if (GUI.Button(new Rect(180, 140, 60, 20), "Pause"))
        {
            Debug.Log("press play2");
            ring2.Play();
            music2.Play();

        }
    }
}

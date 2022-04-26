// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class buttonController : MonoBehaviour
// {
//     private AudioSource audiomv;
//     private ParticleSystem ring;
//     public void Start()
//     {
//         audiomv = GetComponent<AudioSource>();
//         ring = GameObject.Find("ring").GetComponent<ParticleSystem>();
//         ring.Stop();
// }
//     public void Play(AudioSource audioData)
//     {
//         if (audioData != null)
//         {
//             audioData.Play(0);
//         }
//     }
//     private void OnGUI()
//     {
//         GUI.Box(new Rect(160, 80, 100, 100),"Menu");
//         if (GUI.Button(new Rect(180, 110, 60, 20), "Play"))
//         {
//             Debug.Log("press play");
//             ring.Play();
//             audiomv.Play();

//         }
//         if (GUI.Button(new Rect(180, 140, 60, 20), "Pause"))
//         {
//             Debug.Log("press pause");
//             ring.Pause();
//             audiomv.Pause();

//         }
//     }
// }

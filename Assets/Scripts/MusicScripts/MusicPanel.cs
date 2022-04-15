using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPanel : MonoBehaviour
{
	public GameObject[] musicObjects;
	public float tempo;

	private void playAll()
	{
		foreach(GameObject musicObject in musicObjects){
			play(musicObject);
		}
	}

	private void play(GameObject player)
	{
		/*
		MainModel.theModel.startAfter(
			1.2f,
			() => {
				
			}
		);
		*/
	}

}


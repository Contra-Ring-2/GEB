using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPanel : MonoBehaviour
{

	private List<RingControl> rings = new List<RingControl>();
	

	private void OnTriggerEnter(Collider other)
	{
		MusicConsumer mc = other.GetComponent<MusicConsumer>();
		if (mc != null){
			mc.isEnabled = true;
		}

	}

	private void OnTriggerExit(Collider other)
	{
		MusicConsumer mc = other.GetComponent<MusicConsumer>();
		if (mc != null)
		{
			mc.isEnabled = false;
		}
	}

	private void StartExhibition(GameObject player) // 請開始表演
	{
		foreach(RingControl ring in rings){
			ring.Play();
		}

		GetComponent<MusicGroup>().PlayAllMusic();

	}
}


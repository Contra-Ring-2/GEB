using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPanel : MonoBehaviour
{

	private List<RingControl> rings = new List<RingControl>();
	
	public void AddObject(GameObject obj)
	{
		// MusicConsumer music = obj.GetComponent<MusicConsumer>();
		// RingControl ring = obj.GetComponent<RingControl>();

		// GetComponent<MusicGroup>().AddConsumer(music);
		// GetComponent<RingGroup>().AddControl(ring);

		MusicConsumer music = obj.GetComponent<MusicConsumer>();
		music.isEnabled = true;
	}

	public void RemoveObject(GameObject obj)
	{
		// TODO: 
	}

	private void OnTriggerEnter(Collider other)
	{
		// MusicConsumer mc = other.GetComponent<MusicConsumer>();
		// if (mc != null){
		// 	mc.isEnabled = true;
		// }

		if (other.tag == "Player")
        {
			StartExhibition();
        }
	}

	private void OnTriggerExit(Collider other)
	{
		// MusicConsumer mc = other.GetComponent<MusicConsumer>();
		// if (mc != null)
		// {
		// 	mc.isEnabled = false;
		// }
	}

	public void StartExhibition() // ½Ð¶}©lªíºt (一句廢話，好像是開始表演拉)
	{
		MusicGroup musicGroup = GetComponent<MusicGroup>();
		RingGroup ringGroup = GetComponent<RingGroup>();

        ringGroup.PlayAllRings(musicGroup.tempo);

		//GetComponent<MusicGroup>().PlayAllMusic();

		float prepSec = 4 * (60 / GetComponent<MusicGroup>().tempo);

		// TODO:
		MasterModel.TheModel.CallbackInSecond(
			prepSec,
			() => { GetComponent<MusicGroup>().PlayAllMusic(); }
		);

		float startTime = Time.time;
		float spc = 8 * (60 / GetComponent<MusicGroup>().tempo); // is this 8?

		void UpdateExhibition()
        {
			GetComponent<RingGroup>().UpdateAllRingNotes(Time.time - startTime - prepSec, spc);
			MasterModel.TheModel.CallbackWaitingFor(new WaitForFixedUpdate(), UpdateExhibition);
		}

		MasterModel.TheModel.CallbackInSecond(
			0,
			UpdateExhibition
		);
	}

    private void Start()
    {
		
    }

}


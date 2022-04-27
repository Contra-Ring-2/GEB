using System.Collections;
using System.Collections.Generic;
// using System.Linq;
using UnityEngine;

public class MusicPanel : MonoBehaviour
{

	// private List<RingControl> rings = new List<RingControl>();
	
	private List<MusicGroup> musicGroups = new List<MusicGroup>();
	private List<RingGroup> ringGroups = new List<RingGroup>();

	private int currentPlayID = 0;
	
	public void AddObject(MusicConsumer music)
	{
		// MusicConsumer music = obj.GetComponent<MusicConsumer>();
		// RingControl ring = obj.GetComponent<RingControl>();

		// GetComponent<MusicGroup>().AddConsumer(music);
		// GetComponent<RingGroup>().AddControl(ring);

		// MusicConsumer music = obj.GetComponent<MusicConsumer>();
		// music.isEnabled = true;

		music.isEnabled = true;
	}

	public void RemoveObject(MusicConsumer music)
	{
		// TODO: 
		music.isEnabled = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		// MusicConsumer mc = other.GetComponent<MusicConsumer>();
		// if (mc != null){
		// 	mc.isEnabled = true;
		// }

		// if (other.tag == "Player")
        // {
		// 	StartExhibition();
        // }
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
		StopExhibition();

		// MusicGroup musicGroup = GetComponent<MusicGroup>();
		// RingGroup ringGroup = GetComponent<RingGroup>();

		foreach (MusicGroup musicGroup in musicGroups)
		{
			RingGroup ringGroup = musicGroup.GetComponent<RingGroup>();

			ringGroup.PlayAllRings(musicGroup.tempo);
			

			//GetComponent<MusicGroup>().PlayAllMusic();
			
			float prepSec = 4 * (60 / musicGroup.tempo);

			// // Debug:
			// Debug.Log("OBJ: " + (MasterModel.TheModel == null) + " " + prepSec + " " + (musicGroup == null));
			// musicGroup.PlayAllMusic();

			// TODO:
			MasterModel.TheModel.CallbackInSecond(
				prepSec,
				() => { musicGroup.PlayAllMusic(); }
			);

			float startTime = Time.time;
			float spc = 8 * (60 / musicGroup.tempo); // is this 8?

			int exhibitionPlayID = currentPlayID;

			void UpdateExhibition()
			{
				if (exhibitionPlayID != currentPlayID)
				{
					return;
				}

				// Debug.Log("update ring exhibition");

				ringGroup.UpdateAllRingNotes(Time.time - startTime - prepSec, spc);
				MasterModel.TheModel.CallbackWaitingFor(new WaitForFixedUpdate(), UpdateExhibition);
			}

			MasterModel.TheModel.CallbackInSecond(
				0,
				UpdateExhibition
			);
		}
	}

	public void StopExhibition()
	{
		currentPlayID++;

		foreach (MusicGroup musicGroup in musicGroups)
		{
			RingGroup ringGroup = musicGroup.GetComponent<RingGroup>();

			musicGroup.StopAllMusic();
			ringGroup.StopAllRings();
		}
	}

	void SearchAllGroups()
	{
		// musicGroups = transform.GetComponentsInChildren<MusicGroup>().ToList();
		musicGroups = new List<MusicGroup>(transform.GetComponentsInChildren<MusicGroup>());
		foreach (MusicGroup musicGroup in musicGroups)
		{
			ringGroups.Add(musicGroup.GetComponent<RingGroup>());
		}
	}

    private void Start()
    {
		SearchAllGroups();
		Debug.Assert(musicGroups.Count > 0 && ringGroups.Count > 0, "MusicPanel should contain MusicGroups & RingGroups");
		Debug.Assert(!ringGroups.Contains(null), "MusicGroups & RingGroups should be paired");


    }

}

